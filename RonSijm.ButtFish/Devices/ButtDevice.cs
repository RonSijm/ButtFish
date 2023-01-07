using Buttplug.Client;

namespace RonSijm.ButtFish.Devices;

public class ButtDevice : IDeviceAbstraction
{
    private readonly string _name;

    private readonly ButtplugClientDevice _device;

    public ButtDevice(ButtplugClientDevice device)
    {
        _device = device;
        _name = _device.Name;
    }

    public async Task SendDuration(int time)
    {
        await _device.VibrateAsync(1);
        await Task.Delay(time);
        await _device.VibrateAsync(0);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public override string ToString()
    {
        return _name;
    }
}