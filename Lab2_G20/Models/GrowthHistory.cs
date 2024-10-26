using System;

namespace Lab2_G20.Models
{
    public class GrowthHistory
    {
        public int Id { get; set; } // Primary Key
        public int CropId { get; set; } // Foreign Key linking to Crop
        public Crop Crop { get; set; }
        public DateTime DateRecorded { get; set; }
        public string GrowthStage { get; set; }
        public string Notes { get; set; }

    }
}