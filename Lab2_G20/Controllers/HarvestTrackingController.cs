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

            // Hämta alla CropTypes från PlantingSchedules-tabellen
            var cropTypes = await _context.PlantingSchedules
                                           .Select(ps => ps.CropType)
                                           .Distinct()
                                           .ToListAsync();

            // Skapa ett view model som innehåller både crops och cropTypes
            var model = new HarvestTrackingViewModel
            {
                Crops = crops,
                CropTypes = cropTypes
            };

            return View(model); // Skickar modellen till vyn
        }

        public async Task<IActionResult> GetDaysToHarvest(string cropType)
        {
            if (string.IsNullOrEmpty(cropType))
            {
                return BadRequest("CropType is required");
            }

            // Hämta DaysToHarvest från PlantingSchedules-tabellen
            var plantingSchedule = await _context.PlantingSchedules
                .FirstOrDefaultAsync(ps => ps.CropType == cropType);

            if (plantingSchedule == null)
            {
                return NotFound("CropType not found");
            }

            // Returnera DaysToHarvest som JSON
            return Json(new { daysToHarvest = plantingSchedule.DaysToHarvest });
        }


        [HttpPost]
        public async Task<IActionResult> AddCrop(string cropType, DateTime plantingDate, DateTime harvestDate)
        {
            if (ModelState.IsValid)
            {
                var crop = new Crop
                {
                    CropType = cropType,
                    PlantingDate = plantingDate,
                    HarvestDate = harvestDate
                };

                // Lägg till grödan i databasen
                _context.Crops.Add(crop);
                await _context.SaveChangesAsync();

                return RedirectToAction("HarvestTracking");
            }

            return View();
        }
    }
}
