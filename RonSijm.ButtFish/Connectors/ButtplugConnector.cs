using Buttplug.Client;
using Buttplug.Client.Connectors.WebsocketConnector;
using RonSijm.ButtFish.Devices;

namespace RonSijm.ButtFish.Connectors;

public class ButtplugConnector
{
    private readonly ButtplugClient _client = new("ButtPlugClient");

    public async Task<List<IDeviceAbstraction>> GetDiscoveredDevices(string discoverAddress = null)
    {
        try
        {
            Console.WriteLine(discoverAddress == null ? "Starting to scan for ButtPlug Devices..." : $"Attempting to connect to {discoverAddress}...");

            Console.WriteLine("Will return results after 5 seconds.", Color.Green);

            if (discoverAddress == null)
            {
                await _client.ConnectAsync(new ButtplugWebsocketConnector(new Uri("ws://localhost:12345")));
            }
            else
            {
                if (!discoverAddress.Contains("://"))
                {
                    discoverAddress = $"ws://{discoverAddress}";
                }

                await _client.ConnectAsync(new ButtplugWebsocketConnector(new Uri(discoverAddress)));
            }

            await _client.StartScanningAsync();

            // Wait 5 seconds on ButtplugClient to find results
            await Task.Delay(5000);

            return _client.Devices.Select(locatedDevice => new ButtDevice(locatedDevice)).Cast<IDeviceAbstraction>().ToList();
        }
        catch (ButtplugClientConnectorException e)
        {
            var innerException = e.InnerException;
            Console.WriteLine($"ButtplugConnector error: {innerException?.Message.Replace(" More detailed information in inner exception.", string.Empty)}", Color.Red);

            if (innerException?.InnerException?.Message != null)
            {
                Console.WriteLine($"Details: {innerException?.InnerException.Message}", Color.Red);
            }

            Console.WriteLine("Try manually configuring URI for Intiface Central instead", Color.Red);
        }
        catch (Exception e)
        {
            Console.WriteLine($"ButtplugConnector error: {e}", Color.Red);
        }

        return new List<IDeviceAbstraction>();
    }
}