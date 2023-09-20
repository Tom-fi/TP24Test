using Microsoft.EntityFrameworkCore;
using TP24.Domain.Entities;

namespace TP24.Infrastructure
{
    public class EFContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
            ///
        }
    }
}
