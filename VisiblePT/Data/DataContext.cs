using Microsoft.EntityFrameworkCore;
using VisiblePT.Models;

namespace VisiblePT.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(b =>
            {
                b.Property(p => p.Price).HasColumnType("decimal(18,2)");
                b.Property(p => p.DiscountPrice).HasComputedColumnSql("[Price] * (1 - ([DiscountPercent] / 100.0))", stored: false)
                                                .HasColumnType("decimal(18,2)");
            });
        }
    }
}
