using System.Drawing;

namespace RonSijm.ButtFish.Ascii;

public static class AsciiToColerfulOutput
{
    public static void AsciiToConsole(string ascii)
    {
        foreach (var character in ascii)
        {
            if (char.IsUpper(character))
            {
                Colorful.Console.Write(character, Color.DarkSeaGreen);
            }
            else if (char.IsLower(character))
            {
                Colorful.Console.Write(character, Color.CornflowerBlue);
            }
            else
            {

                Colorful.Console.Write(character, Color.SaddleBrown);
            }
        }
    }
}