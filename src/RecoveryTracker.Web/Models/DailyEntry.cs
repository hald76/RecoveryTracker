using System.ComponentModel.DataAnnotations;

namespace RecoveryTracker.Web.Models;

public class DailyEntry
{
    public int Id { get; set; }

    public DateOnly EntryDate { get; set; }

    [Range(0, 10)]
    public int PainLevel { get; set; }

    public string ActivityName { get; set; } = "Walking";

    [Range(1, 180)]
    public int ActivityMinutes { get; set; } = 15;

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

public static class WorkActivities
{
    public const string Other = "Other";
    public static readonly string[] Fixed = { "Walking", "Computer desk work", "Hardware repair", "Moving equipment" };
    public static readonly string[] All = Fixed.Append(Other).ToArray();

    public static string Emoji(string activity) => activity switch
    {
        "Walking" => "🚶",
        "Computer desk work" => "💻",
        "Hardware repair" => "🔧",
        "Moving equipment" => "📦",
        _ => "🏷️"
    };
}
