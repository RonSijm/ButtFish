using System;
using RonSijm.UCIEngineInterlop.Models;

namespace RonSijm.UCIEngineInterlop.Core;

public interface IUCIEngine : IDisposable
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