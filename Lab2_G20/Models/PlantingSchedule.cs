using System;
using System.ComponentModel.DataAnnotations;

namespace Lab2_G20.Models
{
    public class PlantingSchedule
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Crop Type")]
        public string CropType { get; set; }

        [Required]
        [Display(Name = "Planting Date")]
        [DataType(DataType.Date)]
        public DateTime PlantingDate { get; set; }

        [Display(Name = "Optimal Planting Date")]
        [DataType(DataType.Date)]
        public DateTime? OptimalPlantingDate { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public int ReminderDaysBefore { get; set; }
        public DateTime DaysToHarvest { get; set; }
    }
}
