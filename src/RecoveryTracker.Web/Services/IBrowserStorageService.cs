namespace RecoveryTracker.Web.Services;

public interface IBrowserStorageService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value);
}
