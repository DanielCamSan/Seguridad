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

        public DbSet<Doctor> doctors => Set<Doctor>();
                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Hospital)
                .WithMany()
                .HasForeignKey(u => u.hospitalId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

                

            modelBuilder.Entity<Hospital>()
                .HasOne(h => h.Admin)
                .WithMany()
                .HasForeignKey(h => h.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
    


            modelBuilder.Entity<Doctor>();
        }
    }
}
