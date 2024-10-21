using Lab2_G20.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Lab2_G20.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Crop> Crops { get; set; } // DbSet för Crop-modellen

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Ange sökvägen till SQLite-databasen
                var projectPath = AppDomain.CurrentDomain.BaseDirectory;
                var databasePath = Path.Combine(projectPath, "Lab2_G20.db"); // Skapa en SQLite-databasfil

                optionsBuilder.UseSqlite($"Data Source={databasePath};"); // Använd SQLite
            }
        }
    }
}