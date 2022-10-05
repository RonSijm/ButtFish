using System;

namespace RonSijm.UCIEngineInterop.Exceptions;

public class MaxTriesException: Exception
{
    public  MaxTriesException(string msg="") : base(msg) { }
}