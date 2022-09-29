using System.Drawing;
using RonSijm.ButtFish.Encoders;
using RonSijm.ButtFish.Models;
using Stockfish.NET.Core;

namespace RonSijm.ButtFish;

public class ButtFishCore
{
    private IStockfish _stockfish;

    private readonly Options _options;
    private readonly ICharacterEncoder _characterEncoder;

    public ButtFishCore(Options options, ICharacterEncoder characterEncoder)
    {
        _options = options;
        _characterEncoder = characterEncoder;
    }

    public async Task Start()
    {
        Console.WriteLine("Welcome to Buttfish!");
        Console.WriteLine();
        Console.WriteLine($"Using Encoder: {_characterEncoder.GetType().Name}");
        Console.WriteLine("Checking for stockfish executable...");

        var startupPath = Environment.CurrentDirectory;
        var stockfishPath = $"{startupPath}\\stockfish_15_x64_avx2.exe";

        if (!File.Exists(stockfishPath))
        {
            Console.WriteLine("Stockfish is missing");
            Console.WriteLine($"Executable expected to be at '{stockfishPath}'");
            Console.WriteLine("You can download it over here: https://github.com/RonSijm/ButtFish/blob/main/RonSijm.ButtFish/stockfish_15_x64_avx2.exe");
            return;
        }

        var deviceDiscoveryManager = new DeviceDiscoveryManager();
        using var device = await deviceDiscoveryManager.GetDevice();

        _stockfish = new Stockfish.NET.Core.Stockfish(stockfishPath);

        do
        {
            try
            {
                Console.WriteLine();
                Colorful.Console.WriteLine("Set the current chess FEN Position...", Color.Green);
                var fenPosition = Console.ReadLine();

                _stockfish.StartNewGame();
                _stockfish.SetFenPosition(fenPosition);
                Console.WriteLine();

                var (success, boardVisual) = _stockfish.GetBoardVisual();
                Console.WriteLine(boardVisual);

                if (success)
                {
                    Console.WriteLine();
                    Console.WriteLine("Next Best Position:");

                    var nextPosition = _stockfish.GetBestMove();

                    if (_options.EndPositionOnly)
                    {
                        nextPosition = nextPosition.Substring(2, 2);
                    }

                    await SendNextMoveToDevice(nextPosition, device);
                }
            }
            catch (Exception e)
            {
                Colorful.Console.WriteLine("Error occurred.", Color.Red);
                Colorful.Console.WriteLine(e, Color.Red);
                Console.WriteLine("Sometimes the program can recover, otherwise it's probably better to restart.");
            }
        } while (true);
    }

    private async Task SendNextMoveToDevice(string nextPosition, IDeviceAbstraction device)
    {
        Console.WriteLine(nextPosition);

        foreach (var nextPositionChar in nextPosition)
        {
            var morseCodeForChar = _characterEncoder.EncodeCharacter(nextPositionChar);

            Console.Write($"({nextPositionChar})");

            foreach (var morseCodeChar in morseCodeForChar)
            {
                if (morseCodeChar == '.')
                {
                    Console.Write('.');
                    await device.SendDuration(MorseConfig.DotTime);
                }
                else if (morseCodeChar == '-')
                {
                    Console.Write('-');
                    await device.SendDuration(MorseConfig.DashTime);
                }
                else
                {
                    // Should never happen
                    throw new ArgumentException(nameof(morseCodeChar));
                }

                // Wait 1 time unit
                Console.Write(" ");
                await Task.Delay(MorseConfig.SpaceBetweenSymbols);
            }

            // wait 3 time units
            await Task.Delay(MorseConfig.SpaceBetweenLetters);
        }

        Console.WriteLine();
        Console.WriteLine("Finished sending command to device.");
    }
}