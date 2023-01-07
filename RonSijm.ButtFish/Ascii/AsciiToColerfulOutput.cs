namespace RonSijm.ButtFish.Ascii;

public static class AsciiToColorfulOutput
{
    public static void AsciiToConsole(string ascii)
    {
        foreach (var character in ascii)
        {
            if (char.IsUpper(character))
            {
                Console.Write(character, Color.DarkSeaGreen);
            }
            else if (char.IsLower(character))
            {
                Console.Write(character, Color.CornflowerBlue);
            }
            else
            {

                Console.Write(character, Color.SaddleBrown);
            }
        }
    }
}