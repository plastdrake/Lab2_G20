using Microsoft.AspNetCore.Mvc;

namespace Lab2_G20.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            // Här kan ni rendera en inloggningssida om ni vill
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Logga in utan att kontrollera användarnamn och lösenord
            // Logiken för att logga in kan anpassas här

            // Om inloggning lyckas:
            return RedirectToAction("Index", "Home");
        }
    }
}
