using System.ComponentModel.DataAnnotations;

namespace RecoveryTracker.Web.Models;

public class RecoveryProfile
{
    public int Id { get; set; }

    [StringLength(120)]
    public string? SurgeryName { get; set; }

    public DateOnly? SurgeryDate { get; set; }

    [StringLength(120)]
    public string? DoctorName { get; set; }

    public DateOnly? NextAppointmentDate { get; set; }

    public bool DarkMode { get; set; }
}
