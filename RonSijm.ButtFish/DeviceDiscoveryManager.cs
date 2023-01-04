using RonSijm.ButtFish.Connectors;
using RonSijm.ButtFish.Helpers;
using RonSijm.ButtFish.Models;
using Color = System.Drawing.Color;

namespace RonSijm.ButtFish;

public class DeviceDiscoveryManager
{
    public async Task<IDeviceAbstraction> GetDevice()
    {
        IDeviceAbstraction device;

        do
        {
            Console.WriteLine();
            Colorful.Console.WriteLine("How would you like to use this tool?", Color.Green);
            Console.WriteLine("Options:");
            Console.WriteLine("1 - Discover Yeelight or Buttplug devices");
            Console.WriteLine("2 - Manually connect Yeelight IP Address");
            Console.WriteLine("3 - Manually connect Buttplug Server");

            var usageChoice = Console.ReadKey().Key;
            Console.WriteLine();

            device = await GetDeviceByChoice(usageChoice);

            if (device == null)
            {
                Colorful.Console.WriteLine("Invalid choice or could not connect to device.", Color.Red);
                Colorful.Console.WriteLine("Please try again...", Color.Red);
                Console.WriteLine();
            }

        } while (device == null);

        return device;
    }

    private static async Task<IDeviceAbstraction> GetDeviceByChoice(ConsoleKey usageChoice)
    {
        if (usageChoice == ConsoleKey.D1)
        {
            return await GetDeviceFromDiscovery();
        }
        else if (usageChoice == ConsoleKey.D2)
        {
            var yeelightConnector = new YeelightConnector();

            Console.WriteLine();
            Colorful.Console.WriteLine("Please provide Yeelight IP or hostname...", Color.Green);
            var ipAddress = Console.ReadLine();
            Console.WriteLine();
            
            return await yeelightConnector.GetDeviceToUse(ipAddress);
        }
        else if (usageChoice == ConsoleKey.D3)
        {
            var buttplugConnector = new ButtplugConnector();

            Console.WriteLine();
            Colorful.Console.WriteLine("Please provide Buttplug Uri...", Color.Green);
            var serverUri = Console.ReadLine();

            var devices = await buttplugConnector.GetDiscoveredDevices(serverUri);
            return ListDiscoveredDevices(devices);
        }

        return null;
    }

    private static async Task<IDeviceAbstraction> GetDeviceFromDiscovery()
    {
        var connector = new DeviceConnector();

        var discoveredDevices = await connector.DiscoverDevices();
        return ListDiscoveredDevices(discoveredDevices);
    }

    private static IDeviceAbstraction ListDiscoveredDevices(List<IDeviceAbstraction> discoveredDevices)
    {
        if (!discoveredDevices.Any())
        {
            Console.WriteLine();
            Colorful.Console.WriteLine("No devices discovered.", Color.Yellow);
            Console.WriteLine();
            return null;
        }

        Console.WriteLine("Discovered the following devices:");

        for (var index = 0; index < discoveredDevices.Count; index++)
        {
            var deviceAbstraction = discoveredDevices[index];

            Console.WriteLine(deviceAbstraction is YeelightDevice ? $"{(index + 1)} - Yeelight device: {deviceAbstraction}" : $"{(index + 1)} - Butt device: {deviceAbstraction}");
        }

        Colorful.Console.WriteLine("Which device do you want to use?", Color.Green);
        var deviceChoice = Console.ReadKey().Key.ToNumber();

        var device = discoveredDevices[deviceChoice - 1];

        return device;
    }
}