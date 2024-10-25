using System;
using System.Collections.Generic;
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
            ViewData["CropId"] = new SelectList(_context.Crops, "Id", "Id");
            return View();
        }

        // POST: GrowthHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CropId,DateRecorded,GrowthStage,Notes")] GrowthHistory growthHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(growthHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CropId"] = new SelectList(_context.Crops, "Id", "Id", growthHistory.CropId);
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
            ViewData["CropId"] = new SelectList(_context.Crops, "Id", "Id", growthHistory.CropId);
            return View(growthHistory);
        }

        // POST: GrowthHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["CropId"] = new SelectList(_context.Crops, "Id", "Id", growthHistory.CropId);
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
