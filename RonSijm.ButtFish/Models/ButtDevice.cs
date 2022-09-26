using Buttplug;

namespace RonSijm.ButtFish.Models;

public class ButtDevice : IDeviceAbstraction
{
    private readonly ButtplugClientDevice _device;

    public ButtDevice(ButtplugClientDevice device)
    {
        _device = device;
    }

    public async Task SendDuration(int time)
    {
        await _device.SendVibrateCmd(1);
        await Task.Delay(time);
        await _device.SendVibrateCmd(0);
    }

    public void Dispose()
    {
        _device.Dispose();
    }
}