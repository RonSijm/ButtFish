namespace RonSijm.ButtFish.Morse;

/// <summary>
/// *Morse Code timing rules*
/// There are rules to help people distinguish dots from dashes in Morse code.
/// - The length of a dot is 1 time unit.
/// - A dash is 3 time units.
/// - The space between symbols(dots and dashes) of the same letter is 1 time unit.
/// - The space between letters is 3 time units.
/// - The space between words is 7 time units.
/// Source: https://www.codebug.org.uk/learn/step/541/morse-code-timing-rules/
/// </summary>
public static class MorseConfig
{
    public const int Timeunit = 400;

    public const int DotTime = Timeunit;
    public const int DashTime = Timeunit * 3;
    public const int SpaceBetweenSymbols = Timeunit;
    public const int SpaceBetweenLetters = Timeunit * 3;
    public const int SpaceBetweenWords = Timeunit * 7;
}