using Microsoft.JSInterop;

namespace RonSijm.ButtFish.Web.Services;

public class StockfishService : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _stockfishModule;
    private DotNetObjectReference<StockfishService>? _dotNetRef;
    private TaskCompletionSource<string>? _bestMoveTask;

    public event Action<string>? OnBestMoveReceived;
    public event Action<string>? OnEngineInfoReceived;

    public bool IsInitialized { get; private set; }

    public StockfishService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> InitializeAsync()
    {
        try
        {
            _stockfishModule = await _jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./js/stockfish-interop.js");
            
            _dotNetRef = DotNetObjectReference.Create(this);
            IsInitialized = await _stockfishModule.InvokeAsync<bool>("initializeStockfish", _dotNetRef);
            
            return IsInitialized;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to initialize Stockfish: {ex.Message}");
            return false;
        }
    }

    public async Task SetPositionAsync(string fen)
    {
        if (_stockfishModule != null && IsInitialized)
        {
            await _stockfishModule.InvokeVoidAsync("setPosition", fen);
        }
    }

    public async Task SetPositionFromMovesAsync(string[] moves)
    {
        if (_stockfishModule != null && IsInitialized)
        {
            await _stockfishModule.InvokeVoidAsync("setPositionFromMoves", moves);
        }
    }

    public async Task<string?> GetBestMoveAsync(int depth = 15, int? moveTimeMs = null)
    {
        if (_stockfishModule == null || !IsInitialized)
        {
            return null;
        }

        _bestMoveTask = new TaskCompletionSource<string>();

        try
        {
            if (moveTimeMs.HasValue)
            {
                await _stockfishModule.InvokeVoidAsync("go", depth, moveTimeMs.Value);
            }
            else
            {
                await _stockfishModule.InvokeVoidAsync("go", depth, null);
            }

            // Wait for the best move with a timeout
            var timeoutTask = Task.Delay(30000); // 30 second timeout
            var completedTask = await Task.WhenAny(_bestMoveTask.Task, timeoutTask);

            if (completedTask == timeoutTask)
            {
                Console.WriteLine("Stockfish timeout");
                return null;
            }

            return await _bestMoveTask.Task;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting best move: {ex.Message}");
            return null;
        }
        finally
        {
            _bestMoveTask = null;
        }
    }

    public async Task StopAsync()
    {
        if (_stockfishModule != null && IsInitialized)
        {
            await _stockfishModule.InvokeVoidAsync("stop");
        }
    }

    public async Task SetOptionAsync(string name, string value)
    {
        if (_stockfishModule != null && IsInitialized)
        {
            await _stockfishModule.InvokeVoidAsync("setOption", name, value);
        }
    }

    public async Task NewGameAsync()
    {
        if (_stockfishModule != null && IsInitialized)
        {
            await _stockfishModule.InvokeVoidAsync("newGame");
        }
    }

    [JSInvokable]
    public void OnBestMove(string move)
    {
        Console.WriteLine($"Best move received: {move}");
        _bestMoveTask?.TrySetResult(move);
        OnBestMoveReceived?.Invoke(move);
    }

    [JSInvokable]
    public void OnEngineInfo(string info)
    {
        OnEngineInfoReceived?.Invoke(info);
    }

    public async ValueTask DisposeAsync()
    {
        if (_stockfishModule != null)
        {
            try
            {
                await _stockfishModule.InvokeVoidAsync("quit");
            }
            catch
            {
                // Ignore errors during disposal
            }
            await _stockfishModule.DisposeAsync();
        }
        _dotNetRef?.Dispose();
    }
}

