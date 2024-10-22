namespace Lab2_G20.Models
{
    public class HarvestTrackingViewModel
    {
        public IEnumerable<Crop> Crops { get; set; }
        public IEnumerable<string> CropTypes { get; set; }
    }
}