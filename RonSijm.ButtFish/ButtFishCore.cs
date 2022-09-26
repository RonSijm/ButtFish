using System.Drawing;
using RonSijm.ButtFish.Models;
using RonSijm.ButtFish.Morse;
using Stockfish.NET;

namespace RonSijm.ButtFish;

public class ButtFishCore
{
    private IStockfish _stockfish;

    public async Task Start()
    {
        Console.WriteLine("Welcome to Buttfish!");
        Console.WriteLine();
        Console.WriteLine("Checking for stockfish executable...");

        var startupPath = Environment.CurrentDirectory;
        var stockfishPath = $"{startupPath}\\stockfish_15_x64_avx2.exe";

        if (!File.Exists(stockfishPath))
        {
            Console.WriteLine($"Stockfish executable expected to be at '{stockfishPath}'");
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

    private static async Task SendNextMoveToDevice(string nextPosition, IDeviceAbstraction device)
    {
        Console.WriteLine(nextPosition);

        foreach (var nextPositionChar in nextPosition)
        {
            var morseCodeForChar = nextPositionChar.ToMorse();

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