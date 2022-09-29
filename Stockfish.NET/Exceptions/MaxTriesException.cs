using System;

namespace Stockfish.NET.Exceptions
{
    public class MaxTriesException: Exception
    {
        public  MaxTriesException(string msg="") : base(msg) { }
    }
}