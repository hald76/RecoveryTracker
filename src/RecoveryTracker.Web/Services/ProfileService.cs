using RecoveryTracker.Web.Models;

namespace RecoveryTracker.Web.Services;

public class ProfileService : IProfileService
{
    private const string StorageKey = "recoverytracker.profile";

    private readonly IBrowserStorageService _storage;
    public ProfileService(IBrowserStorageService storage) => _storage = storage;

    public async Task<RecoveryProfile> GetAsync() =>
        await _storage.GetAsync<RecoveryProfile>(StorageKey) ?? new RecoveryProfile { Id = 1 };

    public async Task SaveAsync(RecoveryProfile profile)
    {
        profile.Id = 1;
        await _storage.SetAsync(StorageKey, profile);
    }
}
