using System.Text;

namespace RonSijm.ButtFish.Ascii;

public static class BoardToAscii
{
    public static string ToAscii(char[,] model)
    {
        var bob = new StringBuilder();
        bob.AppendLine("   ╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗");

        for (var horizontal = 8 - 1; horizontal >= 0; horizontal--)
        {
            bob.Append(" " + (horizontal + 1) + " ║");

            for (var vertical = 0; vertical < 8; vertical++)
            {
                bob.Append(' ');
                bob.Append(model[horizontal, vertical]);

                if (vertical != 7)
                {
                    bob.Append(" │");
                }
                else
                {
                    bob.Append(' ');
                }
            }

            bob.AppendLine("║");

            bob.AppendLine(horizontal != 0
                ? "   ╟───┼───┼───┼───┼───┼───┼───┼───╢"
                : "   ╚═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╝");
        }

        bob.AppendLine("     a   b   c   d   e   f   g   h  ");

        return bob.ToString();
    }
}