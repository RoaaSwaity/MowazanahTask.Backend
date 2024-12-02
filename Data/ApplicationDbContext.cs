using Microsoft.EntityFrameworkCore;
using Data.Seed;
using Domain.Entities;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Stock> Stocks { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.Property(e => e.MidPrice)
                      .HasPrecision(18, 4);
            });
            
            ////Seeder
            //new SeederManager().Seed(ref modelBuilder);
            ////End Seeder

            base.OnModelCreating(modelBuilder);
        }
    }
}
