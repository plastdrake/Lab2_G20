using System;

namespace Lab2_G20.Models
{
    public class GrowthHistory
    {
        public int Id { get; set; } // Primary Key
        public int CropId { get; set; } // Foreign Key linking to Crop
        public Crop Crop { get; set; } // Navigation property to Crop
        public DateTime DateRecorded { get; set; } // The date when the growth event was recorded
        public string GrowthStage { get; set; } // Description of the growth stage (e.g., "Seedling", "Flowering")
        public string Notes { get; set; } // Optional: Additional notes on the growth event
    }
}
