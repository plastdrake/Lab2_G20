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

        // GET: Display all planting schedules
        public async Task<IActionResult> PlantingSchedule()
        {
            var plantingSchedules = await _context.PlantingSchedules.ToListAsync();
            return View(plantingSchedules);
        }

        // POST: Add a new planting schedule
        [HttpPost]
        public async Task<IActionResult> AddPlantingSchedule(string crop, DateTime plannedPlantingDate, int reminderDaysBefore, int daysToHarvest, string? optimalPlantingDate, string notes)
        {
            if (ModelState.IsValid)
            {
                var plantingSchedule = new PlantingSchedule
                {
                    Crop = crop,
                    PlannedPlantingDate = plannedPlantingDate,
                    ReminderDaysBefore = reminderDaysBefore,
                    DaysToHarvest = daysToHarvest,
                    Notes = notes,
                    OptimalPlantingDate = optimalPlantingDate
                };

                try
                {
                    _context.PlantingSchedules.Add(plantingSchedule);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("PlantingSchedule");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            var plantingSchedules = await _context.PlantingSchedules.ToListAsync();
            return View("PlantingSchedule", plantingSchedules);
        }

        // AJAX: Get the optimal planting date for a specific crop
        [HttpGet]
        public async Task<JsonResult> GetOptimalPlantingDate(string crop)
        {
            var plantingSchedule = await _context.PlantingSchedules.FirstOrDefaultAsync(ps => ps.Crop == crop);

            if (plantingSchedule != null)
            {
                return Json(new { optimalPlantingDate = plantingSchedule.OptimalPlantingDate });
            }

            return Json(new { optimalPlantingDate = "" }); // Return an empty string if no schedule is found
        }

        // GET: Edit planting schedule
        public async Task<IActionResult> Edit(int id)
        {
            var plantingSchedule = await _context.PlantingSchedules.FindAsync(id);
            if (plantingSchedule == null)
            {
                return NotFound();
            }

            return View(plantingSchedule);
        }

        // POST: Update planting schedule
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlantingSchedule plantingSchedule)
        {
            if (id != plantingSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plantingSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantingScheduleExists(plantingSchedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PlantingSchedule));
            }

            return View(plantingSchedule); // Return view if update fails
        }

        // GET: Delete planting schedule confirmation
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantingSchedule = await _context.PlantingSchedules.FirstOrDefaultAsync(m => m.Id == id);
            if (plantingSchedule == null)
            {
                return NotFound();
            }

            return View(plantingSchedule); // Return view to confirm deletion
        }

        // POST: Confirm and delete planting schedule
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plantingSchedule = await _context.PlantingSchedules.FindAsync(id);
            if (plantingSchedule != null)
            {
                _context.PlantingSchedules.Remove(plantingSchedule);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(PlantingSchedule));
        }

        // Helper method to check if a planting schedule exists by ID
        private bool PlantingScheduleExists(int id)
        {
            return _context.PlantingSchedules.Any(e => e.Id == id);
        }
    }
}
