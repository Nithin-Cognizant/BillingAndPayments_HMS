using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoctorManagementSystem.UI.Models
{
    public class DoctorRegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Speciality { get; set; }

        public string Qualification { get; set; }

        [Range(0, 100, ErrorMessage = "Experience must be between 0 and 100")]
        public int? ExperienceYears { get; set; }

        public List<AvailabilityViewModel> Availabilities { get; set; } = new List<AvailabilityViewModel>(); // Initialize here
    }

    public class AvailabilityViewModel
    {
        [Required]
        public string Day { get; set; } = string.Empty;
        [Required]
        public string TimeSlot { get; set; } = string.Empty; // e.g., 10 AM - 12 PM
    }
}