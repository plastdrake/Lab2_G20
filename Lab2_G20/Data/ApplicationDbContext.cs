using Lab2_G20.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Lab2_G20.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
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

                optionsBuilder.UseSqlite($"Data Source={databasePath};");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional configurations if needed
        }
    }
}
