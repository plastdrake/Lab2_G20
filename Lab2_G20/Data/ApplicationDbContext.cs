using Lab2_G20.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2_G20.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Crop> Crops { get; set; } // DbSet för din Crop-modell

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Ange sökvägen till databasfilen i projektmappen
                var projectPath = AppDomain.CurrentDomain.BaseDirectory; // Hämtar projektmappen
                var databasePath = Path.Combine(projectPath, "Lab2_G20.db"); // Skapa en databasfil

                optionsBuilder.UseSqlite($"Data Source={databasePath};"); // Anslutning till SQLite
            }
        }
    }
}