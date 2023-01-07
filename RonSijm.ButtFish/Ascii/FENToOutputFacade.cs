namespace RonSijm.ButtFish.Ascii;

public static class FENToOutputFacade
{
    public static bool PaintBoard(string fenPosition)
    {
        try
        {
            var boardModel = fenPosition.ConvertToCharArray();

            if (boardModel == null)
            {
                return false;
            }

            var whiteToModel = fenPosition.IsWhiteToMove();
            var boardAscii = BoardToAscii.ToAscii(boardModel, whiteToModel);
            AsciiToColorfulOutput.AsciiToConsole(boardAscii);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}", Color.Red);
            return false;
        }
    }
}