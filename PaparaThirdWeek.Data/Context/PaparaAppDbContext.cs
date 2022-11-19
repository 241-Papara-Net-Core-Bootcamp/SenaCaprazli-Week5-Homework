using Microsoft.EntityFrameworkCore;
using PaparaThirdWeek.Data.Configurations;
using PaparaThirdWeek.Domain.Entities;

namespace PaparaThirdWeek.Data.Context
{
    public class PaparaAppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public PaparaAppDbContext(DbContextOptions<PaparaAppDbContext> options):base(options)
        {

        }
        //public PaparaAppDbContext()
        //{

        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
           //DbContext'deki tüm tablo configurationları bulup register yapar
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaparaAppDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=PaparaTestDb;Trusted_Connection=true");

        }


    }
}
