using System.Text.Json;
using Microsoft.JSInterop;

namespace RecoveryTracker.Web.Services;

public class BrowserStorageService : IBrowserStorageService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IJSRuntime _js;
    public BrowserStorageService(IJSRuntime js) => _js = js;

    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await _js.InvokeAsync<string?>("localStorage.getItem", key);
        return json is null ? default : JsonSerializer.Deserialize<T>(json, JsonOptions);
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value, JsonOptions);
        await _js.InvokeVoidAsync("localStorage.setItem", key, json);
    }
}
