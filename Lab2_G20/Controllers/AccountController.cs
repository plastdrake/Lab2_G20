using Microsoft.AspNetCore.Mvc;

namespace Lab2_G20.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Logga in utan att kontrollera användarnamn och lösenord
            // Om inloggning lyckas:
            return RedirectToAction("Index", "Home");
        }
    }
}
