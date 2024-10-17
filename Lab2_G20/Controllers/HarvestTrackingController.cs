using Microsoft.AspNetCore.Mvc;

namespace Lab2_G20.Controllers
{
    public class HarvestTrackingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
