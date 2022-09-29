using Stockfish.NET.Models;

namespace Stockfish.NET.Core
{
    public interface IStockfish
    {
        int Depth { get; set; }
        int SkillLevel { get; set; }
        void SetPosition(params string[] move);
        (bool Success, string Result) GetBoardVisual();
        string GetFenPosition();
        void SetFenPosition(string fenPosition);
        string GetBestMove();
        string GetBestMoveTime(int time = 1000);
        bool IsMoveCorrect(string moveValue);
        Evaluation GetEvaluation();
        void StartNewGame();
    }
}