using Microsoft.AspNetCore.Mvc;

namespace Lab2_G20.Controllers
{
    public class PlantingScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
