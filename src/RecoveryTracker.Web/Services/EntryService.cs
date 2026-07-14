using RecoveryTracker.Web.Models;

namespace RecoveryTracker.Web.Services;

public class EntryService : IEntryService
{
    private const string StorageKey = "recoverytracker.entries";

    private readonly IBrowserStorageService _storage;
    public EntryService(IBrowserStorageService storage) => _storage = storage;

    public async Task<List<DailyEntry>> GetAllAsync()
    {
        var entries = await LoadAsync();
        return entries.OrderBy(e => e.EntryDate).ToList();
    }

    public async Task<List<DailyEntry>> GetInRangeAsync(DateOnly from, DateOnly to)
    {
        var entries = await LoadAsync();
        return entries
            .Where(e => e.EntryDate >= from && e.EntryDate <= to)
            .OrderBy(e => e.EntryDate)
            .ToList();
    }

    public async Task<DailyEntry?> GetByDateAsync(DateOnly date)
    {
        var entries = await LoadAsync();
        return entries.FirstOrDefault(e => e.EntryDate == date);
    }

    public async Task<DailyEntry?> GetAsync(int id)
    {
        var entries = await LoadAsync();
        return entries.FirstOrDefault(e => e.Id == id);
    }

    public async Task<DailyEntry> UpsertAsync(DailyEntry entry)
    {
        var entries = await LoadAsync();
        var existing = entry.Id != 0
            ? entries.FirstOrDefault(e => e.Id == entry.Id)
            : entries.FirstOrDefault(e => e.EntryDate == entry.EntryDate);

        if (existing is null)
        {
            entry.Id = entries.Count == 0 ? 1 : entries.Max(e => e.Id) + 1;
            entry.CreatedUtc = DateTime.UtcNow;
            entry.UpdatedUtc = DateTime.UtcNow;
            entries.Add(entry);
            await _storage.SetAsync(StorageKey, entries);
            return entry;
        }

        existing.EntryDate = entry.EntryDate;
        existing.PainLevel = entry.PainLevel;
        existing.Activities = entry.Activities;
        existing.IceApplied = entry.IceApplied;
        existing.MedicationTaken = entry.MedicationTaken;
        existing.Swelling = entry.Swelling;
        existing.Note = entry.Note;
        existing.UpdatedUtc = DateTime.UtcNow;
        await _storage.SetAsync(StorageKey, entries);
        return existing;
    }

    public async Task DeleteAsync(int id)
    {
        var entries = await LoadAsync();
        entries.RemoveAll(e => e.Id == id);
        await _storage.SetAsync(StorageKey, entries);
    }

    private async Task<List<DailyEntry>> LoadAsync() =>
        await _storage.GetAsync<List<DailyEntry>>(StorageKey) ?? new List<DailyEntry>();
}
