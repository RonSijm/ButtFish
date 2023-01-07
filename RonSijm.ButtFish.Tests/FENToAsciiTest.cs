
// ReSharper disable StringLiteralTypo
namespace RonSijm.ButtFish.Tests;

public class FENToAsciiTest
{
    [Fact]
    public void Should_Be_Able_To_Convert_White_Board_To_Ascii()
    {
        var fenCode = "1bb2QQQ/bbbb1Q2/bbbbbQ2/bbbbbQKQ/bbbbbQ2/kbbb1Q2/rrr2Q2/rrr2Q2 w - - 0 1";
        var boardModel = fenCode.ConvertToCharArray();
        var whiteToModel = fenCode.IsWhiteToMove();

        var boardAscii = BoardToAscii.ToAscii(boardModel, whiteToModel);

        var expected = @"   ╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗
 8 ║   │ b │ b │   │   │ Q │ Q │ Q ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 7 ║ b │ b │ b │ b │   │ Q │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 6 ║ b │ b │ b │ b │ b │ Q │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 5 ║ b │ b │ b │ b │ b │ Q │ K │ Q ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 4 ║ b │ b │ b │ b │ b │ Q │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 3 ║ k │ b │ b │ b │   │ Q │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 2 ║ r │ r │ r │   │   │ Q │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 1 ║ r │ r │ r │   │   │ Q │   │   ║
   ╚═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╝
     a   b   c   d   e   f   g   h  
";

        boardAscii.Should().Be(expected);
    }

    [Fact]
    public void Should_Be_Able_To_Convert_Black_Board_To_Ascii()
    {
        var fenCode = "k7/q1pppp2/p1bP4/r1n1P3/n1r2P1P/b5Pp/P4RpP/RNBQKBNp b Q - 0 1";
        var boardModel = fenCode.ConvertToCharArray();
        var whiteToModel = fenCode.IsWhiteToMove();

        var boardAscii = BoardToAscii.ToAscii(boardModel, whiteToModel);

        var expected = @"   ╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗
 1 ║ p │ N │ B │ K │ Q │ B │ N │ R ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 2 ║ P │ p │ R │   │   │   │   │ P ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 3 ║ p │ P │   │   │   │   │   │ b ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 4 ║ P │   │ P │   │   │ r │   │ n ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 5 ║   │   │   │ P │   │ n │   │ r ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 6 ║   │   │   │   │ P │ b │   │ p ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 7 ║   │   │ p │ p │ p │ p │   │ q ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 8 ║   │   │   │   │   │   │   │ k ║
   ╚═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╝
     h   g   f   e   d   c   b   a  
";

        boardAscii.Should().Be(expected);
    }
}