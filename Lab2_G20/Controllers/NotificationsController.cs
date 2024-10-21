using Microsoft.AspNetCore.Mvc;

namespace Lab2_G20.Controllers
{
    public class NotificationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
