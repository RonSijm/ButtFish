namespace RonSijm.ButtFish;

public class Options
{
    /// <summary>
    /// Option to indicate that you don't want to transmit the start position,
    /// And only want to transmit the end position
    /// </summary>
    public bool EndPositionOnly { get; set; }

    /// <summary>
    /// Option to indicate which encoder you want to use.
    /// Current available:
    /// - MorseEncoder (Default)
    /// - SimplifiedPulse
    /// </summary>
    public string Encoder { get; set; }

    /// <summary>
    /// A list of Engines to use.
    /// </summary>
    public Dictionary<string, string> Engines { get; set; }

    /// <summary>
    /// Sets the Milliseconds for a TimeUnit. Default 400ms
    /// <see cref="TimeUnitConfig"/>
    /// </summary>
    public int TimeUnitInMS { get; set; } = 400;

    /// <summary>
    /// Indicates that you do not want to use a chess engine,
    /// But instead you want to manually input the moves yourself.
    /// </summary>
    public bool UseManualInput { get; set; }
}