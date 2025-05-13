using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Doctor

{
    public string Specialty;
    public List<Availability> Availability;
    public int ExperienceYears;

    public int Id { get; set; }

    [Required]

    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]

    public string Email { get; set; } = string.Empty;

    [Required]

    public string Password { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    [Required]

    public string Speciality { get; set; } = string.Empty;

    [Required]

    public string Qualification { get; set; } = string.Empty;

    public List<Availability>? Availabilities { get; set; }

}

public class Availability

{

    [Key]

    public int Id { get; set; }

    [Required]

    public string Day { get; set; } = string.Empty; // e.g., Monday

    [Required]

    public string TimeSlot { get; set; } = string.Empty; // e.g., 10 AM - 12 PM

    public int DoctorId { get; set; }

    [ForeignKey("DoctorId")]

    public Doctor Doctor { get; set; }

}
