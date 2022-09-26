using System.Drawing;

namespace RonSijm.ButtFish;

public class Program
{
    public static async Task Main(string[] args)
    {
        Colorful.Console.WriteAscii("ButtFish", Color.FromArgb(0, 212, 255));
        Console.WriteLine();

        var core = new ButtFishCore();
        await core.Start();

        Console.ReadKey();
    }
}