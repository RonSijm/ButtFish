using System.Drawing;

namespace RonSijm.ButtFish.Ascii;

public class FENToOutputFacade
{
    public static bool PaintBoard(string fenPosition)
    {
        try
        {
            var boardModel = fenPosition.ConvertToCharArray();
            var boardAscii = BoardToAscii.ToAscii(boardModel);
            AsciiToColerfulOutput.AsciiToConsole(boardAscii);

            return true;
        }
        catch (Exception e)
        {
            Colorful.Console.WriteLine($"Error: {e}", Color.Red);
            return false;
        }
    }
}