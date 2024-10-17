using Lab2_G20.Data;
using Lab2_G20.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Lab2_G20.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Test of database
        public IActionResult Index()
        {
            // Lägg till en ny gröda i databasen
            var newCrop = new Crop
            {
                Name = "Potatoes",
                PlantingDate = DateTime.Now
            };

            _context.Crops.Add(newCrop);
            _context.SaveChanges();

            // Hämta alla grödor från databasen
            var crops = _context.Crops.ToList();

            // Skicka grödorna till vyn eller returnera som JSON för testning
            return Json(crops);
        }
    }
}