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
        public async Task<IActionResult> Create(int CropId, string GrowthStage, string Notes, DateTime DateRecorded)
        {
            // Create a new instance of GrowthHistory
            var growthHistory = new GrowthHistory
            {
                CropId = CropId,
                GrowthStage = GrowthStage,
                Notes = Notes,
                DateRecorded = DateRecorded
            };

            // Validate the model state
            if (ModelState.IsValid)
            {
                _context.Add(growthHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect as necessary
            }

            // Repopulate crops if the model state is invalid
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
            return View(growthHistory); // Return the existing growth history object to pre-fill the form
        }

        // POST: GrowthHistory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int CropId, string GrowthStage, string Notes, DateTime DateRecorded)
        {
            // Create a new instance of GrowthHistory with updated values
            var growthHistory = new GrowthHistory
            {
                Id = id,
                CropId = CropId,
                GrowthStage = GrowthStage,
                Notes = Notes,
                DateRecorded = DateRecorded
            };

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

    }
}
