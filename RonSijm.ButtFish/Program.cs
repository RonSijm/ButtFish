using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RonSijm.ButtFish.InputLoops;

namespace RonSijm.ButtFish;

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once UnusedParameter.Global
// ReSharper disable once StringLiteralTypo
public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteAscii("ButtFish", Color.FromArgb(0, 212, 255));
        Console.WriteLine();

        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
        var options = builder.Get<Options>();

        ICharacterEncoder encoder = options.Encoder is not "SimplifiedPulse" ? new MorseEncoder() : new SimplifiedPulseEncoder();

        TimeUnitConfig.TimeInit = options.TimeUnitInMS;

        var serviceProvider = new ServiceCollection()
            .AddTransient<FENBasedLoop>()
            .AddTransient<ManualInputLoop>()
            .AddTransient<DeviceBroadcaster>()
            .AddSingleton(encoder)
            .AddSingleton(options)
            .BuildServiceProvider();


        Console.WriteLine("Welcome to Buttfish!", Color.GreenYellow);
        Console.WriteLine();
        Console.WriteLine($"Using Encoder: {encoder.GetType().Name}");
        Console.WriteLine(" - Available Options: SimplifiedPulse, MorseEncoder");

        var deviceDiscoveryManager = new DeviceDiscoveryManager();
        var devices = await deviceDiscoveryManager.GetDevice();

        Console.WriteLine();
        Console.WriteLine();

        IInputLoop inputLoop = options.UseManualInput ? serviceProvider.GetRequiredService<ManualInputLoop>() : serviceProvider.GetRequiredService<FENBasedLoop>();

        await inputLoop.Start(devices);

        Console.ReadKey();
    }
}