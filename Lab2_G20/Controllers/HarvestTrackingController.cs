using Lab2_G20.Data;
using Lab2_G20.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2_G20.Controllers
{
    public class HarvestTrackingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HarvestTrackingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> HarvestTracking()
        {
            // Hämta alla grödor från databasen
            var crops = await _context.Crops.ToListAsync();
            // Hämta distinkta grödtyper från PlantingSchedules
            var cropTypes = await _context.PlantingSchedules.Select(ps => ps.Crop).Distinct().ToListAsync();
            ViewBag.CropTypes = cropTypes; // Skickar grödtyper till vyn

            return View(crops); // Skickar grödorna till vyn
        }

        [HttpPost]
        public async Task<IActionResult> AddCrop(string cropType, DateTime plantingDate)
        {
            // Log input values for debugging
            Console.WriteLine($"CropType: {cropType}, PlantingDate: {plantingDate}");

            // Check if the cropType is not empty
            if (string.IsNullOrWhiteSpace(cropType))
            {
                ModelState.AddModelError("CropType", "Crop Type is required.");
            }

            // Check if plantingDate is valid
            if (plantingDate == default)
            {
                ModelState.AddModelError("PlantingDate", "Planting Date is required.");
            }

            if (ModelState.IsValid)
            {
                // Fetch the DaysToHarvest from the PlantingSchedule table
                var plantingSchedule = await _context.PlantingSchedules
                    .FirstOrDefaultAsync(ps => ps.Crop == cropType);

                if (plantingSchedule != null)
                {
                    // Calculate HarvestDate based on DaysToHarvest
                    DateTime harvestDate = plantingDate.AddDays(plantingSchedule.DaysToHarvest);

                    var crop = new Crop
                    {
                        CropType = cropType,
                        PlantingDate = plantingDate,
                        HarvestDate = harvestDate
                    };

                    _context.Crops.Add(crop);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("HarvestTracking");
                }
                else
                {
                    ModelState.AddModelError("", "The selected crop type does not exist in the planting schedule.");
                }
            }
            else
            {
                // Log model state errors
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"Error in {error.Key}: {subError.ErrorMessage}");
                    }
                }
            }

            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetDaysToHarvest(string crop)
        {
            var plantingSchedule = await _context.PlantingSchedules
                .FirstOrDefaultAsync(ps => ps.Crop == crop);

            if (plantingSchedule != null)
            {
                return Json(new { daysToHarvest = plantingSchedule.DaysToHarvest });
            }
            return Json(new { daysToHarvest = 0 });
        }
    }
}
