using System;
using System.Diagnostics;

namespace RonSijm.UCIEngineInterop.Core;

internal class UCIEngineProcess : IDisposable
{
    private ProcessStartInfo ProcessStartInfo { get; }
    private Process Process { get; }

    public UCIEngineProcess(string path)
    {
        ProcessStartInfo = new ProcessStartInfo
        {
            FileName = path,
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };

        Process = new Process {StartInfo = ProcessStartInfo};
    }
        
    public void Wait(int millisecond)
    {
        this.Process.WaitForExit(millisecond);
    }

    public void WriteLine(string command)
    {
        if (Process.StandardInput == null)
        {
            throw new NullReferenceException();
        }
        Process.StandardInput.WriteLine(command);
        Process.StandardInput.Flush();
    }

    public string ReadLine()
    {
        if (Process.StandardOutput == null)
        {
            throw new NullReferenceException();
        }
        return Process.StandardOutput.ReadLine();
    }

    public void Start()
    {
        Process.Start();
    }

    ~UCIEngineProcess()
    {
        Dispose(false);
    }

    private void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        Process?.Close();
        Process?.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}