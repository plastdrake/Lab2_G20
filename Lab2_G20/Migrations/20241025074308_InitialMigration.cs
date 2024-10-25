using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2_G20.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CropType = table.Column<string>(type: "TEXT", nullable: false),
                    PlantingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HarvestDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrowthHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CropType = table.Column<string>(type: "TEXT", nullable: false),
                    PlantingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HarvestDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantingSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Crop = table.Column<string>(type: "TEXT", nullable: true),
                    PlannedPlantingDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OptimalPlantingDate = table.Column<string>(type: "TEXT", nullable: true),
                    ReminderDaysBefore = table.Column<int>(type: "INTEGER", nullable: false),
                    DaysToHarvest = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantingSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserReminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReminderType = table.Column<string>(type: "TEXT", nullable: false),
                    ReminderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReminderNotes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReminders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crops");

            migrationBuilder.DropTable(
                name: "GrowthHistories");

            migrationBuilder.DropTable(
                name: "PlantingSchedules");

            migrationBuilder.DropTable(
                name: "UserReminders");
        }
    }
}
