using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2_G20.Data;
using Lab2_G20.Models;

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
        public async Task<IActionResult> Index(string search)
        {
            var growthHistories = _context.GrowthHistory.Include(g => g.Crop).AsQueryable();

            // Search by crop type or notes
            if (!string.IsNullOrEmpty(search))
            {
                growthHistories = growthHistories.Where(g =>
                    g.Crop.CropType.Contains(search) || g.Notes.Contains(search));
            }

            // Retrieve list of crops with planting and harvested dates
            var cropsWithDates = await _context.Crops
                .Select(c => new
                {
                    c.CropType,
                    c.PlantingDate,
                    c.HarvestDate
                })
                .ToListAsync();

            // Pass the cropsWithDates to the view using ViewBag
            ViewBag.CropsWithDates = cropsWithDates;

            // Return the list of GrowthHistory using the model directly
            return View(await growthHistories.ToListAsync());
        }

        // GET: GrowthHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var growthHistory = await _context.GrowthHistory
                .Include(g => g.Crop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (growthHistory == null)
            {
                return NotFound();
            }

            return View(growthHistory);
        }

        // GET: GrowthHistory/Create
        public IActionResult Create()
        {
            ViewBag.CropTypes = new SelectList(_context.Crops
                .Select(c => new { Id = c.Id, CropType = c.CropType })
                .Distinct(), "Id", "CropType");

            return View();
        }

        // POST: GrowthHistory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CropId,DateRecorded,GrowthStage,Notes")] GrowthHistory growthHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(growthHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CropTypes = new SelectList(_context.Crops
                .Select(c => new { Id = c.Id, CropType = c.CropType })
                .Distinct(), "Id", "CropType");

            return View(growthHistory);
        }

        // GET: GrowthHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var growthHistory = await _context.GrowthHistory.FindAsync(id);
            if (growthHistory == null)
            {
                return NotFound();
            }

            ViewBag.CropTypes = new SelectList(_context.Crops
                .Select(c => new { Id = c.Id, CropType = c.CropType })
                .Distinct(), "Id", "CropType");

            return View(growthHistory);
        }

        // POST: GrowthHistory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CropId,DateRecorded,GrowthStage,Notes")] GrowthHistory growthHistory)
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

            ViewBag.CropTypes = new SelectList(_context.Crops
                .Select(c => new { Id = c.Id, CropType = c.CropType })
                .Distinct(), "Id", "CropType");

            return View(growthHistory);
        }

        // GET: GrowthHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var growthHistory = await _context.GrowthHistory
                .Include(g => g.Crop)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrowthHistoryExists(int id)
        {
            return _context.GrowthHistory.Any(e => e.Id == id);
        }
    }
}
