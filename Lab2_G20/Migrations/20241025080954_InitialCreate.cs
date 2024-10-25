using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2_G20.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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

            migrationBuilder.CreateTable(
                name: "GrowthHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CropId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GrowthStage = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrowthHistory_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrowthHistory_CropId",
                table: "GrowthHistory",
                column: "CropId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrowthHistory");

            migrationBuilder.DropTable(
                name: "PlantingSchedules");

            migrationBuilder.DropTable(
                name: "UserReminders");

            migrationBuilder.DropTable(
                name: "Crops");
        }
    }
}
