using Lab2_G20.Data;
using Lab2_G20.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2_G20.Controllers
{
    public class PlantingScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlantingScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> PlantingSchedule()
        {
            // Get all planting schedules
            var plantingSchedules = await _context.PlantingSchedules.ToListAsync();
            return View(plantingSchedules);
        }

        // Add new planting schedule
        [HttpPost]
        public async Task<IActionResult> AddPlantingSchedule(string crop, DateTime plantingDate, int reminderDaysBefore)
        {
            if (ModelState.IsValid)
            {
                var plantingSchedule = new PlantingSchedule
                {
                    Crop = crop,
                    PlantingDate = plantingDate.ToString("yyyy-MM-dd"),
                    ReminderDaysBefore = reminderDaysBefore,
                    // Automatically calculate the optimal planting date
                    OptimalPlantingDate = plantingDate.AddDays(reminderDaysBefore * -1).ToString("yyyy-MM-dd")
                };

                // Add to database
                _context.PlantingSchedules.Add(plantingSchedule);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetOptimalPlantingDate(string crop)
        {
            var plantingSchedule = await _context.PlantingSchedules
                .FirstOrDefaultAsync(ps => ps.Crop == crop);

            if (plantingSchedule != null)
            {
                return Json(new { optimalPlantingDate = plantingSchedule.OptimalPlantingDate });
            }

            return Json(new { optimalPlantingDate = "" });
        }

        // For editing or deleting records you can add similar methods
    }
}
