using Lab2_G20.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2_G20.Controllers
{
    public class HarvestTrackingController : Controller
    {
        public IActionResult HarvestTracking()
        {
            var crop = new Crop
            {
                PlantingDate = DateTime.Now, // Exempelvärde
                HarvestDate = DateTime.Now.AddDays(30) // Exempelvärde
            };

            return View(crop); // Skickar modellen till vyn
        }
    }

}
