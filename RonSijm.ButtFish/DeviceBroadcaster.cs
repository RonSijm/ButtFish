namespace RonSijm.ButtFish;

public class DeviceBroadcaster
{
    private readonly ICharacterEncoder _characterEncoder;

    public DeviceBroadcaster(ICharacterEncoder characterEncoder)
    {
        _characterEncoder = characterEncoder;
    }

    public async Task SendNextMoveToDevice(string nextPosition, IList<IDeviceAbstraction> devices)
    {
        Console.WriteLine(nextPosition);

        foreach (var nextPositionChar in nextPosition)
        {
            var morseCodeForChar = _characterEncoder.EncodeCharacter(nextPositionChar);

            if (morseCodeForChar == null)
            {
                continue;
            }

            Console.Write($"({nextPositionChar})");

            foreach (var durationToSend in morseCodeForChar.Select(morseCodeChar => morseCodeChar == '.' ? TimeUnitConfig.DotTime : TimeUnitConfig.DashTime))
            {
                Task.WaitAll(devices.Select(device => device.SendDuration(durationToSend)).ToArray());

                // Wait 1 time unit
                Console.Write(" ");
                await Task.Delay(TimeUnitConfig.SpaceBetweenSymbols);
            }

            // wait 3 time units
            await Task.Delay(TimeUnitConfig.SpaceBetweenLetters);
        }

        Console.WriteLine();
        Console.WriteLine("Finished sending command to device.", Color.Green);
    }
}