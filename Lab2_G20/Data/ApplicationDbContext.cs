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

        public DbSet<Crop> Crops { get; set; }
        public DbSet<PlantingSchedule> PlantingSchedules { get; set; }
        public DbSet<UserReminder> UserReminders { get; set; }
        public DbSet<GrowthHistory> GrowthHistory { get; set; }

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

            // Optional: Configure models
        }
    }
}
