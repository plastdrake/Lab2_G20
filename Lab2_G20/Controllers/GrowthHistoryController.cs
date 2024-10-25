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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GrowthHistory.Include(g => g.Crop);
            return View(await applicationDbContext.ToListAsync());
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
            // Fetch distinct crop types with IDs from the PlantingSchedule table
            ViewBag.CropTypes = new SelectList(_context.PlantingSchedules
                .Select(ps => new { Id = ps.Id, CropType = ps.Crop })
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

            // Re-fetch crop types in case of an error
            ViewBag.CropTypes = new SelectList(_context.PlantingSchedules
                .Select(ps => new { Id = ps.Id, CropType = ps.Crop })
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

            // Fetch distinct crop types from the PlantingSchedule table
            ViewBag.CropTypes = new SelectList(_context.PlantingSchedules
                .Select(ps => new { Id = ps.Id, CropType = ps.Crop })
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
