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

        var encoder = GetCharacterEncoder();

        var serviceProvider = new ServiceCollection()
            .AddSingleton<ButtFishCore>()
            .AddSingleton(encoder)
            .BuildServiceProvider();

        var core = serviceProvider.GetRequiredService<ButtFishCore>();
        await core.Start();

        Console.ReadKey();
    }

    private static ICharacterEncoder GetCharacterEncoder()
    {
        var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build();
        var encoderConfig = builder["Encoder"];

        ICharacterEncoder encoder = null;

        encoder = encoderConfig is not "SimplifiedPulse" ? new MorseEncoder() : new SimplifiedPulseEncoder();

        return encoder;
    }
}