using System;

namespace Lab2_G20.Models
{
    public class Crop
    {
        public int Id { get; set; } // Primärnyckel
        public string CropType { get; set; }
        public DateTime PlantingDate { get; set; }
        public DateTime HarvestDate { get; set; }

        // Beräknad egenskap för att få skördedatum baserat på planteringsdatum och dagar till skörd
        public void SetHarvestDate(int daysToHarvest)
        {
            HarvestDate = PlantingDate.AddDays(daysToHarvest);
        }
    }
}