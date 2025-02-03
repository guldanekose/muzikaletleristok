using Microsoft.AspNetCore.Mvc;
using muzikaletleristok.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;

namespace muzikaletleristok.Controllers
{
    [Authorize]//3.adým:
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MuzikaaletleristokContext _MuzikaaletleristokContext;

        public HomeController(ILogger<HomeController> logger, MuzikaaletleristokContext MuzikaaletleristokContext)
        {
            _logger = logger;
            this._MuzikaaletleristokContext = _MuzikaaletleristokContext;
        }

        public IActionResult Index()
        {
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
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }
    }
}
