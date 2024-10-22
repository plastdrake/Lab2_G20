using System;

namespace Lab2_G20.Models
{
    public class Crop
    {
        public int Id { get; set; } // Primärnyckel
        public string Name { get; set; }
        public DateTime PlantingDate { get; set; }
        public DateTime HarvestDate { get; set; }

        // Beräknad egenskap för att få skördedatum baserat på planteringsdatum och dagar till skörd
        public bool SetHarvestDate(int daysToHarvest)
        {
            // Kontrollera om daysToHarvest är ett giltigt värde
            if (daysToHarvest <= 0)
            {
                throw new ArgumentException("Dagar till skörd måste vara större än 0.");
            }

            // Kontrollera om PlantingDate är ett giltigt datum
            if (PlantingDate == DateTime.MinValue)
            {
                throw new InvalidOperationException("Planteringsdatumet är ogiltigt.");
            }

            // Beräkna skördedatum
            DateTime calculatedHarvestDate = PlantingDate.AddDays(daysToHarvest);

            // Kontrollera om skördedatumet är i framtiden
            if (calculatedHarvestDate <= DateTime.Now)
            {
                throw new InvalidOperationException("Skördedatumet måste vara i framtiden.");
            }

            // Om alla kontroller passerar, sätt skördedatumet
            HarvestDate = calculatedHarvestDate;

            return true; // Indikerar att skördedatumet har satts korrekt
        }
    }
}