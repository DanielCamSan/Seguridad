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
            modelBuilder.Entity<Hospital>();
            modelBuilder.Entity<Doctor>(b=>
            {
                b.HasOne(d => d.Hospital)
                 .WithMany(h => h.Doctors)
                 .HasForeignKey(d => d.HospitalId)
                 .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Hospital>()
                .HasOne(h => h.Admin)
                .WithOne(u => u.Hospital)
                .HasForeignKey<Hospital>(h => h.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
