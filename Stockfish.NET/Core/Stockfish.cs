using System;
using System.Collections.Generic;
using System.Linq;
using Stockfish.NET.Exceptions;
using Stockfish.NET.Models;

namespace Stockfish.NET.Core
{
    public class Stockfish : IStockfish
    {
        #region private variables

        /// <summary>
        /// 
        /// </summary>
        private const int MAX_TRIES = 200;

        /// <summary>
        /// 
        /// </summary>
        private int _skillLevel;

        #endregion

        # region private properties

        /// <summary>
        /// 
        /// </summary>
        private StockfishProcess _stockfish { get; set; }

        #endregion

        #region public properties

        /// <summary>
        /// 
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 
        /// </summary>
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

        #endregion

        # region constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="depth"></param>
        /// <param name="settings"></param>
        public Stockfish(
            string path,
            int depth = 2,
            Settings settings = null)
        {
            Depth = depth;
            _stockfish = new StockfishProcess(path);
            _stockfish.Start();
            _stockfish.ReadLine();

            if (settings == null)
            {
                Settings = new Settings();
            }
            else
            {
                Settings = settings;
            }

            SkillLevel = Settings.SkillLevel;
            foreach (var property in Settings.GetPropertiesAsDictionary())
            {
                setOption(property.Key, property.Value);
            }

            StartNewGame();
        }

        #endregion

        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="estimatedTime"></param>
        private void send(string command, int estimatedTime = 100)
        {
            _stockfish.WriteLine(command);
            _stockfish.Wait(estimatedTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MaxTriesException"></exception>
		private bool isReady()
        {
			send("isready");
			var tries = 0;
			while (tries < MAX_TRIES) {
				++tries;

				if (_stockfish.ReadLine() == "readyok") {
					return true;
				}
			}
			throw new MaxTriesException();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ApplicationException"></exception>
        private void setOption(string name, string value)
        {
            send($"setoption name {name} value {value}");
            if (!isReady())
            {
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moves"></param>
        /// <returns></returns>
        private string movesToString(string[] moves)
        {
            return string.Join(" ", moves);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        public void StartNewGame()
        {
            send("ucinewgame");
            if (!isReady())
            {
                throw new ApplicationException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void go()
        {
            send($"go depth {Depth}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        private void goTime(int time)
        {
            send($"go movetime {time}", estimatedTime: time + 100);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<string> readLineAsList()
        {
            var data = _stockfish.ReadLine();
            return data.Split(' ').ToList();
        }

        #endregion

        #region public

        /// <summary>
        /// Setup current position
        /// </summary>
        /// <param name="moves"></param>
        public void SetPosition(params string[] moves)
        {
            StartNewGame();
            send($"position startpos moves {movesToString(moves)}");
        }

        /// <summary>
        /// Get visualisation of current position
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MaxTriesException"></exception>
        public (bool Success, string Result) GetBoardVisual()
        {
            send("d");
            var board = "";
            var lines = 0;
            var tries = 0;
            while (lines < 17)
            {
                if (tries > MAX_TRIES)
                {
                    throw new MaxTriesException();
                }

                var data = _stockfish.ReadLine();

                if (data == null)
                {
                    return (false, "Could not read line. Possibly invalid FEN position");
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

        /// <summary>
        /// Get position in fen format
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MaxTriesException"></exception>
        public string GetFenPosition()
        {
            send("d");
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new MaxTriesException();
                }

                var data = readLineAsList();
                if (data[0] == "Fen:")
                {
                    return string.Join(" ", data.GetRange(1, data.Count - 1));
                }

                tries++;
            }
        }

        /// <summary>
        /// Set position in fen format
        /// </summary>
        /// <param name="fenPosition"></param>
        public void SetFenPosition(string fenPosition)
        {
            StartNewGame();
            send($"position fen {fenPosition}");
        }

        /// <summary>
        /// Getting best move of current position
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MaxTriesException"></exception>
        public string GetBestMove()
        {
            go();
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new MaxTriesException();
                }

                var data = readLineAsList();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        /// <exception cref="MaxTriesException"></exception>
        public string GetBestMoveTime(int time = 1000)
        {
            goTime(time);
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new MaxTriesException();
                }

                var data = readLineAsList();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveValue"></param>
        /// <returns></returns>
        /// <exception cref="MaxTriesException"></exception>
        public bool IsMoveCorrect(string moveValue)
        {
            send($"go depth 1 searchmoves {moveValue}");
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new MaxTriesException();
                }

                var data = readLineAsList();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MaxTriesException"></exception>
        public Evaluation GetEvaluation()
        {
            Evaluation evaluation = new Evaluation();
            var fen = GetFenPosition();
            Color compare;
            // fen sequence for white always contains w
            if (fen.Contains("w"))
            {
                compare = Color.White;
            }
            else
            {
                compare = Color.Black;
            }

            // I'm not sure this is the good way to handle evaluation of position, but why not?
            // Another way we need to somehow limit engine depth? 
            goTime(10000);
            var tries = 0;
            while (true)
            {
                if (tries > MAX_TRIES)
                {
                    throw new MaxTriesException("tries:"+tries+">max-tries:"+MAX_TRIES);
                }

                var data = readLineAsList();
                if (data[0] == "info")
                {
                    for (int i = 0; i < data.Count; i++)
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

        #endregion
    }
}
