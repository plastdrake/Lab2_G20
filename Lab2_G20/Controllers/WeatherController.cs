using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class WeatherController : Controller
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<IActionResult> Index(string city = "Stockholm") // Default city
    {
        var weatherData = await _weatherService.GetWeatherDataAsync(city);
        return View(weatherData);
    }
}
