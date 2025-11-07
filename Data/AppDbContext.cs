using Microsoft.EntityFrameworkCore;
using Security.Models;

namespace Security.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // --- DbSets (Colecciones de Tablas) ---
        public DbSet<User> Users => Set<User>();
        public DbSet<Hospital> Hospitals => Set<Hospital>();
        public DbSet<Doctor> Doctors => Set<Doctor>(); // <--- EJERCICIO 1: Entidad Doctor

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación 1:1 Hospital y User (Admin Asignado)
            modelBuilder.Entity<Hospital>(e =>
            {
                // El Hospital tiene un Admin (User)
                e.HasOne(h => h.Admin)
                 .WithOne() // User no necesita propiedad de navegación de regreso al Hospital
                 .HasForeignKey<Hospital>(h => h.AdminId)
                 .IsRequired(false); // AdminId puede ser nulo inicialmente
            });

            // Relación 1:N Hospital y Doctor
            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(d => d.Id);
                e.Property(d => d.Name).IsRequired();

                // Un Doctor pertenece a un Hospital
                e.HasOne(d => d.Hospital)
                 .WithMany() // Hospital puede tener muchos Doctores
                 .HasForeignKey(d => d.HospitalId)
                 .IsRequired();
            });

            // Mapeo básico para las otras entidades (por si no tienen Fluent API explícita)
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Hospital>();
        }
    }
}