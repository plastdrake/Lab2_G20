using System;

namespace Lab2_G20.Models
{
    public class GrowthHistory
    {
        public int Id { get; set; } // Primary Key
        public string CropType { get; set; }
        public DateTime PlantingDate { get; set; }
        public DateTime HarvestDate { get; set; }
        public int DaysToHarvest => (HarvestDate - PlantingDate).Days;
        public string Comments { get; set; } // Optional: notes on crop progress, yield, etc.
    }
}