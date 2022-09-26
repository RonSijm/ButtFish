using RonSijm.ButtFish.Models;
using YeelightAPI;

namespace RonSijm.ButtFish.Connectors;

public class YeelightConnector
{
    public async Task<List<IDeviceAbstraction>> GetDiscoveredDevices()
    {
        Console.WriteLine("Starting to scan for Yeelight Devices...");
        var discoveredDevices = await DeviceLocator.DiscoverAsync();

        var result = discoveredDevices.Select(locatedDevice => new YeelightDevice(locatedDevice)).Cast<IDeviceAbstraction>().ToList();

        return result;
    }

    public Task<IDeviceAbstraction> GetDeviceToUse(string ipAddress)
    {
        var device = new Device(ipAddress);

        return Task.FromResult<IDeviceAbstraction>(new YeelightDevice(device));
    }
}