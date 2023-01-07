namespace RonSijm.ButtFish.Models;

public interface IDeviceAbstraction : IDisposable
{
    Task SendDuration(int time);
}