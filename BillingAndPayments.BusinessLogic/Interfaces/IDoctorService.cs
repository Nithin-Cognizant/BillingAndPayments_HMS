using BillingAndPayments.BusinessLogic.Services;
using System.Numerics;
using System.Threading.Tasks;
namespace BillingAndPayments.BusinessLogic.Services
{
    public interface IDoctorService
    {
        Task<bool> RegisterDoctorAsync(Doctor doctor);
        // Add this line below the registration method
        Task<Doctor?> AuthenticateDoctorAsync(string email, string password);
    }
}