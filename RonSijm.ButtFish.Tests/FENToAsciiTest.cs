using FluentAssertions;
using RonSijm.ButtFish.Ascii;

namespace RonSijm.ButtFish.Tests;

public class FENToAsciiTest
{
    [Fact]
    public void Should_Be_Able_To_Convert_White_Board_To_Ascii()
    {
        var fenCode = "k7/q1pppp2/p1bP4/r1n1P3/n1r2P1P/b5Pp/P4RpP/RNBQKBNp w Q - 0 1";
        var boardModel = fenCode.ConvertToCharArray();
        var whiteToModel = fenCode.IsWhiteToMove();

        var boardAscii = BoardToAscii.ToAscii(boardModel, whiteToModel);

        var expected = @"   ╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗
 8 ║ k │   │   │   │   │   │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 7 ║ q │   │ p │ p │ p │ p │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 6 ║ p │   │ b │ P │   │   │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 5 ║ r │   │ n │   │ P │   │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 4 ║ n │   │ r │   │   │ P │   │ P ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 3 ║ b │   │   │   │   │   │ P │ p ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 2 ║ P │   │   │   │   │ R │ p │ P ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 1 ║ R │ N │ B │ Q │ K │ B │ N │ p ║
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