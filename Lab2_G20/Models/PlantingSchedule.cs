namespace Lab2_G20.Models
{
    public class PlantingSchedule
    {
        public int Id { get; set; }  // Primary key
        public string? Crop { get; set; }
        public DateTime? PlannedPlantingDate { get; set; }
        public string? OptimalPlantingDate { get; set; }
        public int ReminderDaysBefore { get; set; }
        public int DaysToHarvest { get; set; }
        public string? Notes { get; set; }
    }
}
