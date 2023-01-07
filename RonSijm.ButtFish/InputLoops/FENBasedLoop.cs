using RonSijm.ButtFish.Devices;

namespace RonSijm.ButtFish.InputLoops;

public class FENBasedLoop : IInputLoop
{
    private readonly DeviceBroadcaster _deviceBroadcaster;
    private readonly Options _options;

    private IUCIEngine _iuciEngine;
    private readonly (string Name, string Path) _enginePath;

    public FENBasedLoop(DeviceBroadcaster deviceBroadcaster, Options options)
    {
        _options = options;
        _deviceBroadcaster = deviceBroadcaster;

        _enginePath = EngineSelector.SelectEngine(_options.Engines);

        if (_enginePath.Name == null)
        {
            // Engine Selector has shown error already.
            return;
        }

        _iuciEngine = new UCIEngine(_enginePath.Path);

        Console.WriteLine($"Using engine: {_enginePath.Name}");
    }


    public async Task Start(IList<IDeviceAbstraction> devices)
    {
        do
        {
            try
            {
                Console.WriteLine("Set the current chess FEN Position - then press enter.", Color.Green);
                var fenPosition = Console.ReadLine();

                // If you just send a whitespace, the engine keeps waiting for the rest of the command
                if (string.IsNullOrWhiteSpace(fenPosition))
                {
                    Console.WriteLine("You didn't have to press enter there...", Color.Orange);
                    continue;
                }

                var success = FENToOutputFacade.PaintBoard(fenPosition);

                if (success)
                {
                    _iuciEngine.StartNewGame();
                    _iuciEngine.SetFenPosition(fenPosition);
                    Console.WriteLine();

                    Console.WriteLine();
                    Console.WriteLine("Next Best Position:");

                    var nextPosition = _iuciEngine.GetBestMove();

                    if (nextPosition == null)
                    {
                        Console.WriteLine("Engine was not able to find a next best move", Color.Orange);
                        continue;
                    }

                    if (_options.EndPositionOnly)
                    {
                        nextPosition = nextPosition.Substring(2, 2);
                    }

                    await _deviceBroadcaster.SendNextMoveToDevice(nextPosition, devices);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("That doesn't look right to me...", Color.Red);
                    Console.Write("Try Again - ", Color.Green);
                }
            }
            // Error that happens when the chess-engine gets fucked up somehow.
            catch (MaxTriesException e)
            {
                Console.WriteLine("Error occurred.", Color.Red);
                Console.WriteLine(e, Color.Red);

                Console.WriteLine("Recycling chess engine...", Color.Green);
                _iuciEngine = new UCIEngine(_enginePath.Path);

                // We don't automatically set the old FEN code, otherwise if you added a broken
                // FEN code, the engine gets in a broken loop
                Console.WriteLine("Please try again", Color.Green);
            }
            catch (Exception e)
            {
                Console.WriteLine(); // Blank line because chess moves were not placed on a new line.
                Console.WriteLine("Error occurred.", Color.Red);
                Console.WriteLine(e, Color.Red);

                if (!string.IsNullOrWhiteSpace(e.Source))
                {
                    Console.WriteLine($"Source:{e.Source}", Color.Red);
                }

                Console.WriteLine("Sometimes the program can recover, otherwise it's probably better to restart.");
            }
        } while (true);
        // ReSharper disable once FunctionNeverReturns
    }
}