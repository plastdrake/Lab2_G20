using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2_G20.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2_G20.Data;

namespace Lab2_G20.Controllers
{
    public class GrowthHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GrowthHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GrowthHistory
        public async Task<IActionResult> Index()
        {
            await SeedData();
            return View(await _context.GrowthHistory.Include(g => g.Crop).ToListAsync());
        }

        // GET: GrowthHistory/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var growthHistory = await _context.GrowthHistory.Include(g => g.Crop).FirstOrDefaultAsync(m => m.Id == id);
            if (growthHistory == null)
            {
                return NotFound();
            }
            return View(growthHistory);
        }

        // GET: GrowthHistory/Create
        public async Task<IActionResult> Create()
        {
            // Fetch available crops from the database
            ViewBag.Crops = await _context.Crops.ToListAsync();
            return View(new GrowthHistory
            {
                DateRecorded = DateTime.Now // Automatically set today's date
            });
        }

        // POST: GrowthHistory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GrowthHistory growthHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(growthHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Repopulate crops in case of an error
            ViewBag.Crops = await _context.Crops.ToListAsync();
            return View(growthHistory);
        }

        // GET: GrowthHistory/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var growthHistory = await _context.GrowthHistory.FindAsync(id);
            if (growthHistory == null)
            {
                return NotFound();
            }

            // Fetch available crops from the database
            ViewBag.Crops = await _context.Crops.ToListAsync();
            return View(growthHistory);
        }

        // POST: GrowthHistory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GrowthHistory growthHistory)
        {
            if (id != growthHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(growthHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrowthHistoryExists(growthHistory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // Repopulate crops in case of an error
            ViewBag.Crops = await _context.Crops.ToListAsync();
            return View(growthHistory);
        }

        // GET: GrowthHistory/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var growthHistory = await _context.GrowthHistory.FindAsync(id);
            if (growthHistory == null)
            {
                return NotFound();
            }
            return View(growthHistory);
        }

        // POST: GrowthHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var growthHistory = await _context.GrowthHistory.FindAsync(id);
            if (growthHistory != null)
            {
                _context.GrowthHistory.Remove(growthHistory);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GrowthHistoryExists(int id)
        {
            return _context.GrowthHistory.Any(e => e.Id == id);
        }

        // Seed method to populate GrowthHistory with sample data
        private async Task SeedData()
        {
            // Check if the table is empty
            if (!_context.GrowthHistory.Any())
            {
                var crops = await _context.Crops.ToListAsync(); // Fetch available crops

                // Sample data for growth history
                var growthHistories = new List<GrowthHistory>
                {
                    new GrowthHistory
                    {
                        CropId = crops.First().Id, // Assuming there's at least one crop
                        DateRecorded = DateTime.Now.AddDays(-10),
                        GrowthStage = "Seedling",
                        Notes = "Planted seeds."
                    },
                    new GrowthHistory
                    {
                        CropId = crops.First().Id,
                        DateRecorded = DateTime.Now.AddDays(-5),
                        GrowthStage = "Vegetative",
                        Notes = "The plants are growing well."
                    },
                    new GrowthHistory
                    {
                        CropId = crops.First().Id,
                        DateRecorded = DateTime.Now,
                        GrowthStage = "Flowering",
                        Notes = "Plants have started to flower."
                    }
                };

                // Add the sample data to the context and save changes
                await _context.GrowthHistory.AddRangeAsync(growthHistories);
                await _context.SaveChangesAsync();
            }
        }
    }
}
