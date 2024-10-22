using Lab2_G20.Models; // Ensure your models are correctly referenced
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

        // DbSets for your models
        public DbSet<Crop> Crops { get; set; } // DbSet for Crop model
        public DbSet<PlantingSchedule> PlantingSchedules { get; set; } // DbSet for PlantingSchedule model

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Specify the path to the SQLite database
                var projectPath = AppDomain.CurrentDomain.BaseDirectory;
                var databasePath = Path.Combine(projectPath, "Lab2_G20.db"); // Create an SQLite database file

                optionsBuilder.UseSqlite($"Data Source={databasePath};"); // Use SQLite
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: You can configure your models here if needed
            // e.g., modelBuilder.Entity<PlantingSchedule>().ToTable("PlantingSchedules");
        }
    }
}
