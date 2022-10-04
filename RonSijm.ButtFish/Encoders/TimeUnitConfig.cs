namespace RonSijm.ButtFish.Encoders;

/// <summary>
/// Config for TimeUnits.
/// *Morse Code timing rules*
/// There are rules to help people distinguish dots from dashes in Morse code.
/// - The length of a dot is 1 time unit.
/// - A dash is 3 time units.
/// - The space between symbols(dots and dashes) of the same letter is 1 time unit.
/// - The space between letters is 3 time units.
/// - The space between words is 7 time units.
/// Source: https://www.codebug.org.uk/learn/step/541/morse-code-timing-rules/
/// </summary>
public static class TimeUnitConfig
{
    public static int TimeInit { get; set; }

    public static readonly int DotTime = TimeInit;
    public static readonly int DashTime = TimeInit * 3;
    public static readonly int SpaceBetweenSymbols = TimeInit;
    public static readonly int SpaceBetweenLetters = TimeInit * 3;
    public static readonly int SpaceBetweenWords = TimeInit * 7;
}