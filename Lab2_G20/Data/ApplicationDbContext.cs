using Lab2_G20.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab2_G20.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Crop> Crops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Hårdkodad anslutningssträng för SQLite
                var connectionString = "Data Source=Lab2_G20.db;";

                optionsBuilder.UseSqlite(connectionString);
            }
        }
    }
}