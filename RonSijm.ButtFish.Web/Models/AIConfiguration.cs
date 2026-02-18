namespace RonSijm.ButtFish.Web.Models;

public class AIConfiguration
{
    public int SkillLevel { get; set; } = 10;
    public int Depth { get; set; } = 10;
    public int MoveTimeMs { get; set; } = 1000;
    public bool LimitStrength { get; set; } = false;
    public int EloRating { get; set; } = 1500;
    public int Threads { get; set; } = 1;
}