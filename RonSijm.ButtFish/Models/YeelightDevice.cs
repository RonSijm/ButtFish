using YeelightAPI;

namespace RonSijm.ButtFish.Models;

public class YeelightDevice : IDeviceAbstraction
{
    public override string ToString()
    {
        return _selectedDevice == null ? "Inner device null" : _selectedDevice.Hostname;
    }

    private readonly Device _selectedDevice;

    public YeelightDevice(Device selectedDevice)
    {
        _selectedDevice = selectedDevice;
    }

    public async Task SendDuration(int time)
    {
        if (!_selectedDevice.IsConnected)
        {
            await _selectedDevice.Connect();
        }

        await _selectedDevice.SetBrightness(100, 1);
        await Task.Delay(time);
        await _selectedDevice.SetBrightness(1, 1);
    }

    public void Dispose()
    {
        _selectedDevice.Dispose();
    }
}