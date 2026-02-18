using Microsoft.JSInterop;

namespace RonSijm.ButtFish.Web.Services;

public class ButtplugService : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _buttplugModule;
    private bool _isConnected;
    private List<DeviceInfo> _devices = new();

    public event Action? OnConnectionChanged;
    public event Action<List<DeviceInfo>>? OnDevicesChanged;

    public bool IsConnected => _isConnected;
    public IReadOnlyList<DeviceInfo> Devices => _devices.AsReadOnly();

    public ButtplugService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        _buttplugModule = await _jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./js/buttplug-interop.js");
    }

    public async Task<bool> ConnectAsync(string serverUrl = "ws://localhost:12345")
    {
        if (_buttplugModule == null)
        {
            await InitializeAsync();
        }

        try
        {
            var result = await _buttplugModule!.InvokeAsync<bool>("connect", serverUrl);
            _isConnected = result;
            OnConnectionChanged?.Invoke();
            return result;
        }
        catch (Exception)
        {
            _isConnected = false;
            OnConnectionChanged?.Invoke();
            return false;
        }
    }

    public async Task DisconnectAsync()
    {
        if (_buttplugModule != null && _isConnected)
        {
            await _buttplugModule.InvokeVoidAsync("disconnect");
            _isConnected = false;
            _devices.Clear();
            OnConnectionChanged?.Invoke();
            OnDevicesChanged?.Invoke(_devices);
        }
    }

    public async Task StartScanningAsync()
    {
        if (_buttplugModule != null && _isConnected)
        {
            await _buttplugModule.InvokeVoidAsync("startScanning");
        }
    }

    public async Task StopScanningAsync()
    {
        if (_buttplugModule != null && _isConnected)
        {
            await _buttplugModule.InvokeVoidAsync("stopScanning");
        }
    }

    public async Task<List<DeviceInfo>> GetDevicesAsync()
    {
        if (_buttplugModule != null && _isConnected)
        {
            var devices = await _buttplugModule.InvokeAsync<DeviceInfo[]>("getDevices");
            _devices = devices.ToList();
            OnDevicesChanged?.Invoke(_devices);
            return _devices;
        }
        return new List<DeviceInfo>();
    }

    public async Task VibrateAsync(int deviceIndex, double intensity, int durationMs)
    {
        if (_buttplugModule != null && _isConnected)
        {
            await _buttplugModule.InvokeVoidAsync("vibrate", deviceIndex, intensity, durationMs);
        }
    }

    public async Task SendMorseCodeAsync(string move, ICharacterEncoder encoder)
    {
        foreach (var character in move.ToLower())
        {
            var morseCode = encoder.EncodeCharacter(character);
            if (morseCode == null) continue;

            foreach (var symbol in morseCode)
            {
                if (symbol == ' ') continue;

                var duration = symbol == '.' ? TimeUnitConfig.DotTime : TimeUnitConfig.DashTime;
                
                foreach (var device in _devices)
                {
                    await VibrateAsync(device.Index, 1.0, duration);
                }

                await Task.Delay(duration + TimeUnitConfig.SpaceBetweenSymbols);
            }

            await Task.Delay(TimeUnitConfig.SpaceBetweenLetters);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_buttplugModule != null)
        {
            await DisconnectAsync();
            await _buttplugModule.DisposeAsync();
        }
    }
}

public class DeviceInfo
{
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
}