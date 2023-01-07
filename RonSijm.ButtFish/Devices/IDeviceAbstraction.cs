namespace RonSijm.ButtFish.Devices;

public interface IDeviceAbstraction : IDisposable
{
    Task SendDuration(int time);
}