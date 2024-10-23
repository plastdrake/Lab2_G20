namespace Lab2_G20.Models
{
    public class PlantingSchedule
    {
        public int Id { get; set; }  // Primary key
        public string Crop { get; set; } // Crop type
        public string PlantingDate { get; set; }  // Date of planting
        public string OptimalPlantingDate { get; set; }  // Calculated optimal planting date
        public int ReminderDaysBefore { get; set; }  // Days before planting to send reminders
        public string DaysToHarvest { get; set; }  // Number of days till harvest
        public string Notes { get; set; }  // Any additional notes about the crop
    }
}
