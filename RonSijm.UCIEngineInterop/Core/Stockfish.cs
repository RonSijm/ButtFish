using System;
using System.Collections.Generic;
using System.Linq;
using RonSijm.UCIEngineInterlop.Exceptions;
using RonSijm.UCIEngineInterlop.Models;

namespace RonSijm.UCIEngineInterlop.Core;

public class UCIEngine : IUCIEngine
{
    public bool BoardVisualUnsupported { get; set; }

    private const int MaxTries = 200;

    private int _skillLevel;

    private UCIEngineProcess UCIEngineProcess { get; set; }

    public Settings Settings { get; set; }

    public int Depth { get; set; }

    public int SkillLevel
    {
        get => _skillLevel;
        set
        {
            _skillLevel = value;
            Settings.SkillLevel = SkillLevel;
            setOption("Skill level", SkillLevel.ToString());
        }
    }

    public UCIEngine(string path, int depth = 2, Settings settings = null)
    {
        Depth = depth;
        UCIEngineProcess = new UCIEngineProcess(path);
        UCIEngineProcess.Start();

        Settings = settings ?? new Settings();

        SkillLevel = Settings.SkillLevel;
        foreach (var property in Settings.GetPropertiesAsDictionary())
        {
            setOption(property.Key, property.Value);
        }

        StartNewGame();
    }

    private void Send(string command, int estimatedTime = 100)
    {
        UCIEngineProcess.WriteLine(command);
        UCIEngineProcess.Wait(estimatedTime);
    }

    private bool IsReady()
    {
        Send("isready");
        var tries = 0;

        while (tries < MaxTries)
        {
            ++tries;

            if (UCIEngineProcess.ReadLine() == "readyok")
            {
                return true;
            }
        }

        throw new MaxTriesException();
    }

    private void setOption(string name, string value)
    {
        Send($"setoption name {name} value {value}");

        if (!IsReady())
        {
            throw new ApplicationException();
        }
    }

    private string MovesToString(string[] moves)
    {
        return string.Join(" ", moves);
    }


    public void StartNewGame()
    {
        Send("ucinewgame");

        if (!IsReady())
        {
            throw new ApplicationException();
        }
    }

    private void Go()
    {
        Send($"go depth {Depth}");
    }

    private void GoTime(int time)
    {
        Send($"go movetime {time}", estimatedTime: time + 100);
    }

    private List<string> ReadLineAsList()
    {
        var data = UCIEngineProcess.ReadLine();
        return data.Split(' ').ToList();
    }

    public void SetPosition(params string[] moves)
    {
        StartNewGame();
        Send($"position startpos moves {MovesToString(moves)}");
    }

    public (bool Success, string Result) GetBoardVisual()
    {
        if (BoardVisualUnsupported)
        {
            return (true, null);
        }

        Send("d");
        var board = "";
        var lines = 0;
        var tries = 0;

        while (lines < 17)
        {
            if (tries > MaxTries)
            {
                throw new MaxTriesException();
            }

            var data = UCIEngineProcess.ReadLine();

            if (data == null)
            {
                return (false, "Could not read line. Possibly invalid FEN position");
            }

            if (data == "error Unknown command: d")
            {
                BoardVisualUnsupported = true;
                return (true, "board visual unsupported.");
            }

            if (data.Contains("+") || data.Contains("|"))
            {
                lines++;
                board += $"{data}\n";
            }

            tries++;
        }

        return (true, board);
    }

    public string GetFenPosition()
    {
        Send("d");
        var tries = 0;
        while (true)
        {
            if (tries > MaxTries)
            {
                throw new MaxTriesException();
            }

            var data = ReadLineAsList();
            if (data[0] == "Fen:")
            {
                return string.Join(" ", data.GetRange(1, data.Count - 1));
            }

            tries++;
        }
    }

    public void SetFenPosition(string fenPosition)
    {
        StartNewGame();
        Send($"position fen {fenPosition}");
    }

    public string GetBestMove()
    {
        Go();
        var tries = 0;
        while (true)
        {
            if (tries > MaxTries)
            {
                throw new MaxTriesException();
            }

            var data = ReadLineAsList();

            if (data[0] == "bestmove")
            {
                if (data[1] == "(none)")
                {
                    return null;
                }

                return data[1];
            }

            tries++;
        }
    }


    public string GetBestMoveTime(int time = 1000)
    {
        GoTime(time);
        var tries = 0;
        while (true)
        {
            if (tries > MaxTries)
            {
                throw new MaxTriesException();
            }

            var data = ReadLineAsList();
            if (data[0] == "bestmove")
            {
                if (data[1] == "(none)")
                {
                    return null;
                }

                return data[1];
            }
        }
    }


    public bool IsMoveCorrect(string moveValue)
    {
        Send($"go depth 1 searchmoves {moveValue}");
        var tries = 0;
        while (true)
        {
            if (tries > MaxTries)
            {
                throw new MaxTriesException();
            }

            var data = ReadLineAsList();
            if (data[0] == "bestmove")
            {
                if (data[1] == "(none)")
                {
                    return false;
                }

                return true;
            }

            tries++;
        }
    }

    public Evaluation GetEvaluation()
    {
        Evaluation evaluation = null;
        var fen = GetFenPosition();

        // fen sequence for white always contains w
        var compare = fen.Contains("w") ? Color.White : Color.Black;

        // I'm not sure this is the good way to handle evaluation of position, but why not?
        // Another way we need to somehow limit engine depth? 
        GoTime(10000);
        var tries = 0;
        while (true)
        {
            if (tries > MaxTries)
            {
                throw new MaxTriesException($"tries:{tries}>max-tries:{MaxTries}");
            }

            var data = ReadLineAsList();
            if (data[0] == "info")
            {
                for (var i = 0; i < data.Count; i++)
                {
                    if (data[i] == "score")
                    {
                        //don't use ternary operator here for readability
                        int k;
                        if (compare == Color.White)
                        {
                            k = 1;
                        }
                        else
                        {
                            k = -1;
                        }

                        evaluation = new Evaluation(data[i + 1], Convert.ToInt32(data[i + 2]) * k);
                    }
                }
            }

            if (data[0] == "bestmove")
            {
                return evaluation;
            }

            tries++;
        }
    }

    public void Dispose()
    {
        UCIEngineProcess?.Dispose();
    }
}