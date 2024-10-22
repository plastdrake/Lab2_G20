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

            return View(crops); // Skickar grödorna till vyn
        }

        [HttpPost]
        public async Task<IActionResult> AddCrop(string cropType, DateTime plantingDate, int daysToHarvest)
        {
            if (ModelState.IsValid)
            {
                var crop = new Crop
                {
                    CropType = cropType,
                    PlantingDate = plantingDate
                };
                crop.SetHarvestDate(daysToHarvest);

                // Lägg till grödan i databasen
                _context.Crops.Add(crop);
                await _context.SaveChangesAsync();
                return RedirectToAction("HarvestTracking");
            }
            return View();
        }
    }
}
