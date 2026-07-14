using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RecoveryTracker.Web.Models;

public class DailyEntry
{
    public int Id { get; set; }

    public DateOnly EntryDate { get; set; }

    [Range(0, 10)]
    public int PainLevel { get; set; }

    public List<ActivityEntry> Activities { get; set; } = new();

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

[JsonConverter(typeof(ActivityEntryJsonConverter))]
public class ActivityEntry
{
    public string Name { get; set; } = "";

    [Range(1, 180)]
    public int Minutes { get; set; } = 15;
}

// Old entries stored Activities as a plain string array (no duration). Reading a bare
// string here upgrades it to an ActivityEntry with a default duration instead of throwing
// and losing the whole entry.
public class ActivityEntryJsonConverter : JsonConverter<ActivityEntry>
{
    public override ActivityEntry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
            return new ActivityEntry { Name = reader.GetString() ?? "", Minutes = 15 };

        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        var name = root.TryGetProperty("name", out var n) ? n.GetString() ?? "" : "";
        var minutes = root.TryGetProperty("minutes", out var m) && m.TryGetInt32(out var mi) ? mi : 15;
        return new ActivityEntry { Name = name, Minutes = minutes };
    }

    public override void Write(Utf8JsonWriter writer, ActivityEntry value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", value.Name);
        writer.WriteNumber("minutes", value.Minutes);
        writer.WriteEndObject();
    }
}

public static class WorkActivities
{
    public static readonly string[] Fixed = { "Walking", "Computer desk work", "Hardware repair", "Moving equipment" };

    public static string Emoji(string activity) => activity switch
    {
        "Walking" => "🚶",
        "Computer desk work" => "💻",
        "Hardware repair" => "🔧",
        "Moving equipment" => "📦",
        _ => "🏷️"
    };
}
