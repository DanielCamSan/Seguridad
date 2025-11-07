using Microsoft.EntityFrameworkCore;
using Security.Models;
using System.Xml.Linq;

namespace Security.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Hospital> Hospitals => Set<Hospital>();
        public DbSet<Doctor> Doctors => Set<Doctor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>();

            modelBuilder.Entity<Hospital>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Name).IsRequired();
                b.Property(x => x.Address).IsRequired().HasMaxLength(200); ;
                b.Property(x => x.Type).IsRequired().HasMaxLength(100); ;

                
            });
            modelBuilder.Entity<Doctor>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.HospitalId).IsRequired();
                b.Property(x => x.Name).IsRequired().HasMaxLength(100);
                b.Property(x => x.Specialty).IsRequired().HasMaxLength(100);

                b.HasOne(o => o.Hospital)
                    .WithOne(p => p.Doctor)
                    .HasForeignKey<Hospital>(p => p.DoctorlId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
