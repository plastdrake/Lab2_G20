using Lab2_G20.Data; // Your DbContext
using Lab2_G20.Models; // Your Models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET: PlantingSchedule
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.PlantingSchedules.ToListAsync(); // Ensure the using directive for EF Core
            return View(schedules);
        }

        // Other actions (Create, Edit, etc.) would follow here...
    }
}
