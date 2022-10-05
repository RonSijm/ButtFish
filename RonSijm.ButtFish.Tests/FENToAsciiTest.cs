using FluentAssertions;
using RonSijm.ButtFish.Ascii;

namespace RonSijm.ButtFish.Tests;

public class FENToAsciiTest
{
    [Fact]
    public void Should_Be_Able_To_Convert_Board_To_Ascii()
    {
        var fenCode = "rnb1kbnr/pppp1ppp/8/4p1q1/5P2/4PQ2/PPPP2PP/RNB1KBNR b KQkq - 2 3";
        var boardModel = fenCode.ConvertToCharArray();
        var boardAscii = BoardToAscii.ToAscii(boardModel);

        var expected = @"   ╔═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╗
 8 ║ R │ N │ B │   │ K │ B │ N │ R ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 7 ║ P │ P │ P │ P │   │   │ P │ P ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 6 ║   │   │   │   │ P │ Q │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 5 ║   │   │   │   │   │ P │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 4 ║   │   │   │   │ p │   │ q │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 3 ║   │   │   │   │   │   │   │   ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 2 ║ p │ p │ p │ p │   │ p │ p │ p ║
   ╟───┼───┼───┼───┼───┼───┼───┼───╢
 1 ║ r │ n │ b │   │ k │ b │ n │ r ║
   ╚═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╝
     a   b   c   d   e   f   g   h  
";

        boardAscii.Should().Be(expected);
    }
}