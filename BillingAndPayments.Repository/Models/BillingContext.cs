using Microsoft.EntityFrameworkCore;

namespace BillingAndPayments.Repository.Models
{
    public class BillingContext : DbContext
    {

        public BillingContext(DbContextOptions<BillingContext> options) : base(options) { }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Availability> Availabilities { get; set; }

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