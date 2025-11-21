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

        public DbSet<Book> Books => Set<Book>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>();
        }
    }
}
