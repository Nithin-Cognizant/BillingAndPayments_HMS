using BillingAndPayments.Repository.Interfaces;
using BillingAndPayments.Repository.Models;
using Microsoft.EntityFrameworkCore;
namespace BillingAndPayments.Repository.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _context;
    public DoctorRepository(AppDbContext context)
    {
        _context = context;
    }
    // Method to add a doctor asynchronously
    public async Task AddDoctorAsync(Doctor doctor)
    {
        if (doctor == null)
        {
            throw new ArgumentNullException(nameof(doctor), "Doctor cannot be null");
        }
        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
    }
    // Method to get a doctor by email asynchronously
    public async Task<Doctor?> GetDoctorByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        }
        return await _context.Doctors.FirstOrDefaultAsync(d => d.Email == email);
    }
}