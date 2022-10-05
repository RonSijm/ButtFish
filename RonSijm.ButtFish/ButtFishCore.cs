using System.Drawing;
using RonSijm.ButtFish.Ascii;
using RonSijm.ButtFish.Encoders;
using RonSijm.ButtFish.Models;
using RonSijm.UCIEngineInterop.Core;

namespace RonSijm.ButtFish;

public class ButtFishCore
{
    private IUCIEngine _iuciEngine;

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
        Console.WriteLine("Checking engines");

        var enginePath = EngineSelector.SelectEngine(_options.Engines);

        if (enginePath.Name == null)
        {
            // Engine Selector has shown error already.
            return;
        }

        Console.WriteLine($"Using engine: {enginePath.Name}");

        var deviceDiscoveryManager = new DeviceDiscoveryManager();
        using var device = await deviceDiscoveryManager.GetDevice();

        _iuciEngine = new UCIEngine(enginePath.Path);

        do
        {
            try
            {
                Console.WriteLine();
                Colorful.Console.WriteLine("Set the current chess FEN Position...", Color.Green);
                var fenPosition = Console.ReadLine();

                _iuciEngine.StartNewGame();
                _iuciEngine.SetFenPosition(fenPosition);
                Console.WriteLine();

                var success = FENToOutputFacade.PaintBoard(fenPosition);

                if (success)
                {
                    Console.WriteLine();
                    Console.WriteLine("Next Best Position:");

                    var nextPosition = _iuciEngine.GetBestMove();

                    if (_options.EndPositionOnly)
                    {
                        nextPosition = nextPosition.Substring(2, 2);
                    }

                    await SendNextMoveToDevice(nextPosition, device);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(); // Blank line because chess moves were not placed on a new line.
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
                    await device.SendDuration(TimeUnitConfig.DotTime);
                }
                else if (morseCodeChar == '-')
                {
                    Console.Write('-');
                    await device.SendDuration(TimeUnitConfig.DashTime);
                }
                else
                {
                    // Should never happen
                    throw new ArgumentException(nameof(morseCodeChar));
                }

                // Wait 1 time unit
                Console.Write(" ");
                await Task.Delay(TimeUnitConfig.SpaceBetweenSymbols);
            }

            // wait 3 time units
            await Task.Delay(TimeUnitConfig.SpaceBetweenLetters);
        }

        Console.WriteLine();
        Console.WriteLine("Finished sending command to device.");
    }
}