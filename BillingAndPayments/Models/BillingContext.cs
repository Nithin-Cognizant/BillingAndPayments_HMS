using Microsoft.EntityFrameworkCore;

namespace BillingAndPayments.Models
{
    public class BillingContext : DbContext
    {

        public BillingContext(DbContextOptions<BillingContext> options) : base(options) { }

        public DbSet<Bill> Bills { get; set; }

    }
}