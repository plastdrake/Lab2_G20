using Microsoft.AspNetCore.Mvc;

namespace Lab2_G20.Controllers
{
    public class GrowthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
