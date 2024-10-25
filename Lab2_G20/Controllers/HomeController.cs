using Lab2_G20.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lab2_G20.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WeatherService _weatherService;
        private readonly NotificationsController _notificationsController;

        public HomeController(ILogger<HomeController> logger, WeatherService weatherService, NotificationsController notificationsController)
        {
            _logger = logger;
            _weatherService = weatherService;
            _notificationsController = notificationsController;
        }

        public async Task<IActionResult> Index()
        {
            var weatherData = await _weatherService.GetWeatherDataAsync(); // Fetch weather data
            ViewBag.Weather = weatherData; // Store it in ViewBag

            // Check if a reminder is due and set it in ViewBag
            ViewBag.IsReminderDue = _notificationsController.CheckIfReminderDue();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
