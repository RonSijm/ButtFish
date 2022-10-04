using System.Drawing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RonSijm.ButtFish.Encoders;

namespace RonSijm.ButtFish;

public class Program
{
    public static async Task Main(string[] args)
    {
        Colorful.Console.WriteAscii("ButtFish", Color.FromArgb(0, 212, 255));
        Console.WriteLine();

        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
        var options = builder.Get<Options>();

        ICharacterEncoder encoder = options.Encoder is not "SimplifiedPulse" ? new MorseEncoder() : new SimplifiedPulseEncoder();

        TimeUnitConfig.TimeInit = options.TimeUnitInMS;

        var serviceProvider = new ServiceCollection()
            .AddSingleton<ButtFishCore>()
            .AddSingleton(encoder)
            .AddSingleton(options)
            .BuildServiceProvider();

        var core = serviceProvider.GetRequiredService<ButtFishCore>();
        await core.Start();

        Console.ReadKey();
    }
}