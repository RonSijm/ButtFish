namespace RonSijm.ButtFish.Devices;

public class ConsoleOutputDevice : IDeviceAbstraction
{
    public Task SendDuration(int time)
    {
        if (time == TimeUnitConfig.DotTime)
        {
            Console.Write('.');
        }
        else if (time == TimeUnitConfig.DashTime)
        {
            Console.Write('-');
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {

    }
}