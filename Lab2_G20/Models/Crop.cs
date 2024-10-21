using System.ComponentModel.DataAnnotations;

namespace Lab2_G20.Models
{
    public class Crop
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PlantingDate { get; set; }
        public DateTime HarvestDate { get; set; }
    }
}
