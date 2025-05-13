using Microsoft.AspNetCore.Identity;
using BillingAndPayments.Repository.Interfaces;
using BillingAndPayments.BusinessLogic.Interfaces;
namespace BillingAndPayments.BusinessLogic.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;
        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> RegisterDoctorAsync(Doctor doctor)
        {
            var existing = await _repository.GetDoctorByEmailAsync(doctor.Email);
            if (existing != null) return false;
            var hasher = new PasswordHasher<Doctor>();
            doctor.Password = hasher.HashPassword(doctor, doctor.Password);  // Hashing here
            await _repository.AddDoctorAsync(doctor);
            return true;
        }
        public async Task<Doctor?> AuthenticateDoctorAsync(string email, string password)
        {
            var doctor = await _repository.GetDoctorByEmailAsync(email);
            if (doctor == null) return null;
            var hasher = new PasswordHasher<Doctor>();
            var result = hasher.VerifyHashedPassword(doctor, doctor.Password, password);
            return result == PasswordVerificationResult.Success ? doctor : null;
        }

    }
}