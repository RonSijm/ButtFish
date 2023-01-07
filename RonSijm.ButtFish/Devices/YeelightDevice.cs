using YeelightAPI;

namespace RonSijm.ButtFish.Devices;

public class YeelightDevice : IDeviceAbstraction
{
    public override string ToString()
    {
        if (_selectedDevice == null)
        {
            return "Inner device null";
        }

        var displayName = _selectedDevice.Hostname;
        var stateResult = _selectedDevice.Properties.TryGetValue("power", out var state);

        if (stateResult)
        {
            if (state.ToString() == "on")
            {
                displayName += " - Light is On";
            }
        }

        return displayName;
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

        // Not sure how non-RGB devices behave when sending this command
        // So putting it in a try/catch
        try
        {
            if (time == TimeUnitConfig.DotTime)
            {
                await _selectedDevice.SetRGBColor(255, 0, 0);
            }
            else
            {
                await _selectedDevice.SetRGBColor(0, 255, 0);
            }
        }
        catch (Exception)
        {
            // Do nothing
        }

        await _selectedDevice.SetBrightness(100, 1);
        await Task.Delay(time);
        await _selectedDevice.SetBrightness(1, 1);
    }

    public void Dispose()
    {
        _selectedDevice.Dispose();
        GC.SuppressFinalize(this);
    }
}