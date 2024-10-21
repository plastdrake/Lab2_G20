using Lab2_G20.Data;
using Lab2_G20.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lab2_G20.Controllers
{
    public class CropController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CropController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Read: Visa en lista över alla grödor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crops.ToListAsync());
        }

        // Update: GET (Visa redigeringsformulär för en specifik gröda)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
            {
                return NotFound();
            }
            return View(crop);
        }

        // Update: POST (Uppdatera grödan i databasen)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Crop crop)
        {
            if (id != crop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(crop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CropExists(crop.Id))
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
            return View(crop);
        }

        // Delete: GET (Bekräfta radering av en specifik gröda)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crop = await _context.Crops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crop == null)
            {
                return NotFound();
            }

            return View(crop);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crop = await _context.Crops.FindAsync(id);
            if (crop != null)
            {
                _context.Crops.Remove(crop);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Redirects to Index or another page after deletion
        }


        private bool CropExists(int id)
        {
            return _context.Crops.Any(e => e.Id == id);
        }
    }
}
