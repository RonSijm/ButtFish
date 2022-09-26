using RonSijm.ButtFish.Models;

namespace RonSijm.ButtFish.Connectors;

public class DeviceConnector
{
    public async Task<List<IDeviceAbstraction>> DiscoverDevices()
    {
        var results = new List<IDeviceAbstraction>();

        var yeelightConnector = new YeelightConnector();
        var yeelightDevices = await yeelightConnector.GetDiscoveredDevices();
        results.AddRange(yeelightDevices);

        var buttplugConnector = new ButtplugConnector();
        var buttplugDevices = await buttplugConnector.GetDiscoveredDevices();
        results.AddRange(buttplugDevices);

        return results;
    }
}