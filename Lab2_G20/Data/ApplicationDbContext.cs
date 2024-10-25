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
        public DbSet<Crop> Crops { get; set; }
        public DbSet<PlantingSchedule> PlantingSchedules { get; set; }

        // Add the DbSet for UserReminders
        public DbSet<UserReminder> UserReminders { get; set; }  // Added UserReminders DbSet

        // Growth history DbSet
        public DbSet<GrowthHistory> GrowthHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var projectPath = AppDomain.CurrentDomain.BaseDirectory;
                var databasePath = Path.Combine(projectPath, "Lab2_G20.db");

                optionsBuilder.UseSqlite($"Data Source={databasePath};"); // Use SQLite
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Configure your models here if needed
        }
    }
}
