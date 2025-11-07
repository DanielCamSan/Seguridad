using Microsoft.EntityFrameworkCore;
using Security.Models;

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
            modelBuilder.Entity<Doctor>();

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Hospital)
                .WithMany(h => h.Doctors)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Hospital)
                .WithMany(h => h.Users) 
                .HasForeignKey(u => u.HospitalId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}