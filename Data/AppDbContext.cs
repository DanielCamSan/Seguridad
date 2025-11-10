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
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Hospital>()
                .HasMany(h => h.Doctors)
                .WithOne(d => d.Hospital)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Hospital>()
                .HasOne(h => h.Admin)
                .WithOne(u => u.Hospital)
                .HasForeignKey<Hospital>(h => h.AdminId) //FK hacia user.Id
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
                entity.Property(d => d.Speciallity).HasMaxLength(100);
            });

        }
    }
}
