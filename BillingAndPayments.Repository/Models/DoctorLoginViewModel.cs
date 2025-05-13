using System.ComponentModel.DataAnnotations;
namespace DoctorManagementSystem.UI.Models
{
    public class DoctorLoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}