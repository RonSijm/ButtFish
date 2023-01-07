using RonSijm.ButtFish.Devices;

namespace RonSijm.ButtFish;

public class DeviceDiscoveryManager
{
    public async Task<IList<IDeviceAbstraction>> GetDevice()
    {
        IList<IDeviceAbstraction> device;

        do
        {
            Console.WriteLine();
            Console.WriteLine("How would you like to use this tool?", Color.Green);
            Console.WriteLine("Options:");
            Console.WriteLine("1 - Discover Yeelight or Buttplug devices");
            Console.WriteLine("2 - Manually connect Yeelight IP Address");
            Console.WriteLine("3 - Manually connect Buttplug Server");

            var usageChoice = Console.ReadKey().Key;
            Console.WriteLine();

            device = await GetDeviceByChoice(usageChoice);

            if (device == null)
            {
                Console.WriteLine("Invalid choice or could not connect to device.", Color.Red);
                Console.WriteLine("Please try again...", Color.Red);
                Console.WriteLine();
            }

        } while (device == null);

        device.Add(new ConsoleOutputDevice());
        return device;
    }

    private static async Task<IList<IDeviceAbstraction>> GetDeviceByChoice(ConsoleKey usageChoice)
    {
        if (usageChoice == ConsoleKey.D1)
        {
            return await GetDeviceFromDiscovery();
        }

        if (usageChoice == ConsoleKey.D2)
        {
            var yeelightConnector = new YeelightConnector();

            Console.WriteLine();
            Console.WriteLine("Please provide Yeelight IP or hostname...", Color.Green);
            var ipAddress = Console.ReadLine();
            Console.WriteLine();

            var device = await yeelightConnector.GetDeviceToUse(ipAddress);

            return new List<IDeviceAbstraction>(){ device };
        }

        if (usageChoice == ConsoleKey.D3)
        {
            var buttplugConnector = new ButtplugConnector();

            Console.WriteLine();
            Console.WriteLine("Please provide Buttplug Uri...", Color.Green);
            var serverUri = Console.ReadLine();

            var devices = await buttplugConnector.GetDiscoveredDevices(serverUri);
            return ListDiscoveredDevices(devices);
        }

        return null;
    }

    private static async Task<IList<IDeviceAbstraction>> GetDeviceFromDiscovery()
    {
        var connector = new DeviceConnector();

        var discoveredDevices = await connector.DiscoverDevices();
        return ListDiscoveredDevices(discoveredDevices);
    }

    private static List<IDeviceAbstraction> ListDiscoveredDevices(List<IDeviceAbstraction> discoveredDevices)
    {
        if (!discoveredDevices.Any())
        {
            Console.WriteLine();
            Console.WriteLine("No devices discovered.", Color.Yellow);
            Console.WriteLine();
            return null;
        }

        Console.WriteLine();
        Console.WriteLine("Discovered the following devices:", Color.Green);

        for (var index = 0; index < discoveredDevices.Count; index++)
        {
            var deviceAbstraction = discoveredDevices[index];

            Console.WriteLine(deviceAbstraction is YeelightDevice ? $"{(index + 1)} - Yeelight device: {deviceAbstraction}" : $"{(index + 1)} - Butt device: {deviceAbstraction}");
        }

        Console.WriteLine();
        Console.WriteLine("Which devices do you want to use?", Color.Green);
        Console.WriteLine("Separate the devices you want to use by a space - then press enter.", Color.Green);

        var deviceChoices = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(deviceChoices))
        {
            return null;
        }

        var devicesSplit = deviceChoices.Split(" ");

        var output = new List<IDeviceAbstraction>();

        foreach (var deviceString in devicesSplit)
        {
            var deviceIntParseResult = int.TryParse(deviceString, out var deviceId);

            if (!deviceIntParseResult)
            {
                Console.WriteLine($"'{deviceString}' is an invalid option.", Color.Red);
            }

            var device = discoveredDevices[deviceId - 1];

            output.Add(device);
        }

        return output;
    }
}