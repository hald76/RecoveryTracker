using System.ComponentModel.DataAnnotations;

namespace RecoveryTracker.Web.Models;

public class DailyEntry
{
    public int Id { get; set; }

    public DateOnly EntryDate { get; set; }

    [Range(0, 10)]
    public int PainLevel { get; set; }

    [Range(0, 600)]
    public int WalkingMinutes { get; set; }

    public bool IceApplied { get; set; }

    public bool MedicationTaken { get; set; }

    public SwellingLevel Swelling { get; set; } = SwellingLevel.None;

    [StringLength(1000)]
    public string? Note { get; set; }

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;
}

public enum SwellingLevel
{
    None = 0,
    Mild = 1,
    Moderate = 2,
    Severe = 3
}
