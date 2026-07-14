using RecoveryTracker.Web.Models;

namespace RecoveryTracker.Web.Services;

public interface IEntryService
{
    Task<List<DailyEntry>> GetAllAsync();
    Task<List<DailyEntry>> GetInRangeAsync(DateOnly from, DateOnly to);
    Task<DailyEntry?> GetByDateAsync(DateOnly date);
    Task<DailyEntry?> GetAsync(int id);
    Task<DailyEntry> UpsertAsync(DailyEntry entry);
    Task DeleteAsync(int id);
}
