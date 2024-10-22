using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2_G20.Migrations
{
    /// <inheritdoc />
    public partial class PlantingSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlantingSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CropType = table.Column<string>(type: "TEXT", nullable: false),
                    PlantingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OptimalPlantingDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    ReminderDaysBefore = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantingSchedules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantingSchedules");
        }
    }
}
