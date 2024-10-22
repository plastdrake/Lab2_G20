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
            var cropTypes = await _context.PlantingSchedules.Select(ps => ps.CropType).Distinct().ToListAsync();
            ViewBag.CropTypes = cropTypes; // Skickar grödtyper till vyn

            return View(crops); // Skickar grödorna till vyn
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

        [HttpGet]
        public async Task<JsonResult> GetDaysToHarvest(string cropType)
        {
            var plantingSchedule = await _context.PlantingSchedules
                .FirstOrDefaultAsync(ps => ps.CropType == cropType);

            if (plantingSchedule != null)
            {
                return Json(new { daysToHarvest = plantingSchedule.DaysToHarvest });
            }
            return Json(new { daysToHarvest = 0 });
        }
    }
}
