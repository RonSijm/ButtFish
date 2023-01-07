namespace RonSijm.ButtFish.InputLoops;

public interface IInputLoop
{
    Task Start(IList<IDeviceAbstraction> devices);
}