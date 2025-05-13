using BillingAndPayments.Repository.Interfaces;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
namespace BillingAndPayments.Repository.Interfaces  
{
    public interface IDoctorRepository
    {
        Task AddDoctorAsync(Doctor doctor);
        Task<Doctor?> GetDoctorByEmailAsync(string email);
    }
}
