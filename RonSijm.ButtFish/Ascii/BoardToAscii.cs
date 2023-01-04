using System.Text;

namespace RonSijm.ButtFish.Ascii;

public static class BoardToAscii
{
    public static string ToAscii(char[,] model, bool isWhiteToMove)
    {
        var lastLoop = isWhiteToMove ? 7 : 0;

        var bob = new StringBuilder();
        bob.AppendLine("   ╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗");

        foreach (var horizontal in Loop.Between(0, 8, !isWhiteToMove))
        {
            bob.Append(" " + (8 - horizontal) + " ║");

            foreach (var vertical in Loop.Between(0, 8, !isWhiteToMove))
            {
                bob.Append(' ');
                bob.Append(model[horizontal, vertical]);

                if (vertical != lastLoop)
                {
                    bob.Append(" │");
                }
                else
                {
                    bob.Append(' ');
                }
            }

            bob.AppendLine("║");

            bob.AppendLine(horizontal != lastLoop
                ? "   ╟───┼───┼───┼───┼───┼───┼───┼───╢"
                : "   ╚═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╝");
        }

        bob.AppendLine(isWhiteToMove ? "     a   b   c   d   e   f   g   h  " : "     h   g   f   e   d   c   b   a  ");

        return bob.ToString();
    }
}