namespace RonSijm.UCIEngineInterop.Models;

public class Settings
{
    private int Threads { get; }
    private bool Ponder { get; }
    private int MultiPV { get; }
    public int SkillLevel { get; }
    private int MoveOverhead { get; }
    private int SlowMover { get; }
    private bool UCIChess960 { get; }

    public Settings(int threads = 0, bool ponder = false, int multiPV = 1, int skillLevel = 20, int moveOverhead = 30, int slowMover = 80, bool uciChess960 = false)
    {
        Ponder = ponder;
        Threads = threads;
        MultiPV = multiPV;
        SkillLevel = skillLevel;
        MoveOverhead = moveOverhead;
        SlowMover = slowMover;
        UCIChess960 = uciChess960;
    }

    public Dictionary<string, string> GetPropertiesAsDictionary()
    {
        return new Dictionary<string, string>
        {
            ["Threads"] = Threads.ToString(),
            ["Ponder"] = Ponder.ToString(),
            ["MultiPV"] = MultiPV.ToString(),
            ["Skill Level"] = SkillLevel.ToString(),
            ["Move Overhead"] = MoveOverhead.ToString(),
            ["Slow Mover"] = SlowMover.ToString(),
            ["UCI_Chess960"] = UCIChess960.ToString(),
        };
    }
}