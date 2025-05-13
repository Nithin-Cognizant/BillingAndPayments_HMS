using System.Numerics;
using BillingAndPayments.Repository.Interfaces; 
using Microsoft.EntityFrameworkCore;

namespace BillingAndPayments.Repository.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Availability> Availabilities { get; set; }

        //outlines different configurations like constraints that define the table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // One-to-many relationship: One doctor has many availability slots
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Availabilities)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId);
        }
    }
}