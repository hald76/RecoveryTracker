using RecoveryTracker.Web.Models;

namespace RecoveryTracker.Web.Services;

public interface IProfileService
{
    Task<RecoveryProfile> GetAsync();
    Task SaveAsync(RecoveryProfile profile);
}
