using System;

namespace Lab2_G20.Models
{
    public class Crop
    {
        public int Id { get; set; } // Primärnyckel
        public string CropType { get; set; }
        public DateTime PlantingDate { get; set; }
        public DateTime HarvestDate { get; set; }
        public ICollection<GrowthHistory>? GrowthHistories { get; set; }
    }
}