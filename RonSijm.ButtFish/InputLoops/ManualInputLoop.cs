namespace RonSijm.ButtFish.InputLoops;

public class ManualInputLoop : IInputLoop
{
    private readonly DeviceBroadcaster _deviceBroadcaster;

    public ManualInputLoop(DeviceBroadcaster deviceBroadcaster)
    {
        _deviceBroadcaster = deviceBroadcaster;
    }

    public async Task Start(IList<IDeviceAbstraction> devices)
    {
        Console.WriteLine("Using Manual move input");

        do
        {
            try
            {
                Console.WriteLine("What's the next move?", Color.Green);
                var nextPosition = Console.ReadLine();

                // If you just send a whitespace, the engine keeps waiting for the rest of the command
                if (string.IsNullOrWhiteSpace(nextPosition))
                {
                    Console.WriteLine("You didn't have to press enter there...", Color.Orange);
                    continue;
                }

                await _deviceBroadcaster.SendNextMoveToDevice(nextPosition, devices);
                Console.WriteLine();
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
            }
        } while (true);
        // ReSharper disable once FunctionNeverReturns
    }
}