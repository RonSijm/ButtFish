using System;

namespace RonSijm.UCIEngineInterlop.Exceptions;

public class MaxTriesException: Exception
{
    public  MaxTriesException(string msg="") : base(msg) { }
}