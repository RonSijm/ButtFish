using Microsoft.JSInterop;

namespace RonSijm.ButtFish.Web.Services;

public class ChessService : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _chessModule;
    private DotNetObjectReference<ChessService>? _dotNetRef;

    public event Action? OnBoardChanged;

    public ChessService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        _chessModule = await _jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/chess-interop.js");
        
        _dotNetRef = DotNetObjectReference.Create(this);
        await _chessModule.InvokeAsync<bool>("initializeChess", _dotNetRef);
    }

    public async Task NewGameAsync()
    {
        if (_chessModule != null)
        {
            await _chessModule.InvokeVoidAsync("newGame");
            OnBoardChanged?.Invoke();
        }
    }

    public async Task<bool> LoadFenAsync(string fen)
    {
        if (_chessModule != null)
        {
            var result = await _chessModule.InvokeAsync<bool>("loadFen", fen);
            if (result)
            {
                OnBoardChanged?.Invoke();
            }
            return result;
        }
        return false;
    }

    public async Task<string?> GetFenAsync()
    {
        if (_chessModule != null)
        {
            return await _chessModule.InvokeAsync<string?>("getFen");
        }
        return null;
    }

    public async Task<string?> GetPgnAsync()
    {
        if (_chessModule != null)
        {
            return await _chessModule.InvokeAsync<string?>("getPgn");
        }
        return null;
    }

    public async Task<ChessMove?> MakeMoveAsync(string from, string to, string promotion = "q")
    {
        if (_chessModule != null)
        {
            var move = await _chessModule.InvokeAsync<ChessMove?>("makeMove", from, to, promotion);
            if (move != null)
            {
                OnBoardChanged?.Invoke();
            }
            return move;
        }
        return null;
    }

    public async Task<List<ChessMove>> GetLegalMovesAsync(string? square = null)
    {
        if (_chessModule != null)
        {
            var moves = await _chessModule.InvokeAsync<ChessMove[]>("getLegalMoves", square);
            return moves?.ToList() ?? new List<ChessMove>();
        }
        return new List<ChessMove>();
    }

    public async Task<bool> IsGameOverAsync()
    {
        if (_chessModule != null)
        {
            return await _chessModule.InvokeAsync<bool>("isGameOver");
        }
        return false;
    }

    public async Task<bool> IsCheckAsync()
    {
        if (_chessModule != null)
        {
            return await _chessModule.InvokeAsync<bool>("isCheck");
        }
        return false;
    }

    public async Task<bool> IsCheckmateAsync()
    {
        if (_chessModule != null)
        {
            return await _chessModule.InvokeAsync<bool>("isCheckmate");
        }
        return false;
    }

    public async Task<string> GetTurnAsync()
    {
        if (_chessModule != null)
        {
            return await _chessModule.InvokeAsync<string>("getTurn");
        }
        return "w";
    }

    public async Task<bool> UndoMoveAsync()
    {
        if (_chessModule != null)
        {
            var result = await _chessModule.InvokeAsync<bool>("undoMove");
            if (result)
            {
                OnBoardChanged?.Invoke();
            }
            return result;
        }
        return false;
    }

    public async Task<Dictionary<string, string>> GetBoardPositionAsync()
    {
        if (_chessModule != null)
        {
            return await _chessModule.InvokeAsync<Dictionary<string, string>>("getBoardPosition");
        }
        return new Dictionary<string, string>();
    }

    public async ValueTask DisposeAsync()
    {
        if (_chessModule != null)
        {
            await _chessModule.DisposeAsync();
        }
        _dotNetRef?.Dispose();
    }
}

public class ChessMove
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Piece { get; set; } = string.Empty;
    public string? Captured { get; set; }
    public string? Promotion { get; set; }
    public string San { get; set; } = string.Empty;
}

