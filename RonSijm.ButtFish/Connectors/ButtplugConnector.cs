using Buttplug;
using RonSijm.ButtFish.Models;

namespace RonSijm.ButtFish.Connectors;

public class ButtplugConnector
{
    private readonly ButtplugClient _client = new("ButtPlugClient");

    public async Task<List<IDeviceAbstraction>> GetDiscoveredDevices(string discoverAddress = null)
    {
        Console.WriteLine("Starting to scan for ButtPlug Devices...");
        Console.WriteLine("Will return results after 5 seconds.");

        if (discoverAddress == null)
        {
            await _client.ConnectAsync(new ButtplugEmbeddedConnectorOptions());
        }
        else
        {
            await _client.ConnectAsync(new ButtplugWebsocketConnectorOptions(new Uri(discoverAddress)));
        }

        await _client.StartScanningAsync();

        // Wait 5 seconds on ButtplugClient to find results
        await Task.Delay(5000);

        return _client.Devices.Select(locatedDevice => new ButtDevice(locatedDevice)).Cast<IDeviceAbstraction>().ToList();
    }
}