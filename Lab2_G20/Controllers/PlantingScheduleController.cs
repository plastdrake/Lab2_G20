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
            // Get all planting schedules from the database
            var plantingSchedules = await _context.PlantingSchedules.ToListAsync();
            return View(plantingSchedules);
        }

        // POST: Add a new planting schedule
        [HttpPost]
        public async Task<IActionResult> AddPlantingSchedule(string crop, DateTime plantingDate, int reminderDaysBefore, int daysToHarvest, string optimalPlantingDate, string notes)
        {
            if (ModelState.IsValid)
            {
                var plantingSchedule = new PlantingSchedule
                {
                    Crop = crop,
                    PlannedPlantingDate = plantingDate.ToString("yyyy-MM-dd"),
                    ReminderDaysBefore = reminderDaysBefore,
                    DaysToHarvest = daysToHarvest,
                    Notes = notes,
                    OptimalPlantingDate = optimalPlantingDate // Store the optimal planting date passed from the form
                };

                // Add the new planting schedule to the database
                _context.PlantingSchedules.Add(plantingSchedule);
                await _context.SaveChangesAsync();

                // Redirect back to the main schedule page after successful creation
                return RedirectToAction("PlantingSchedule");
            }

            // If ModelState is not valid, return the same PlantingSchedule view but pass the current schedules
            var plantingSchedules = await _context.PlantingSchedules.ToListAsync();
            return View("PlantingSchedule", plantingSchedules);
        }

        // AJAX: Get the optimal planting date for a specific crop
        [HttpGet]
        public async Task<JsonResult> GetOptimalPlantingDate(string crop)
        {
            // Retrieve the planting schedule for the specified crop
            var plantingSchedule = await _context.PlantingSchedules.FirstOrDefaultAsync(ps => ps.Crop == crop);

            if (plantingSchedule != null)
            {
                return Json(new { optimalPlantingDate = plantingSchedule.OptimalPlantingDate });
            }

            return Json(new { optimalPlantingDate = "" }); // Return an empty string if no schedule is found
        }

        // GET: Edit planting schedule (display the edit form - implementation will depend on your requirements)
        public async Task<IActionResult> Edit(int id)
        {
            var plantingSchedule = await _context.PlantingSchedules.FindAsync(id);
            if (plantingSchedule == null)
            {
                return NotFound();
            }

            // Return the view for editing the schedule (you'll need to create an Edit view)
            return View(plantingSchedule);
        }

        // POST: Update planting schedule (save the changes to the database)
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
        public async Task<IActionResult> Delete(int id)
        {
            var plantingSchedule = await _context.PlantingSchedules.FindAsync(id);
            if (plantingSchedule == null)
            {
                return NotFound();
            }

            return View(plantingSchedule); // Return view to confirm deletion
        }

        // POST: Confirm and delete planting schedule
        [HttpPost, ActionName("Delete")]
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