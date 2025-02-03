using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using muzikaletleristok.Models;

namespace muzikaletleristok.Controllers
{
    public class StartpController : Controller
    {
     
        public IActionResult Login()

{

ClaimsPrincipal claimuser = HttpContext.User;
if (claimuser.Identity.IsAuthenticated)
return RedirectToAction("Index", "Home");
return View();
    }
    [HttpPost]
        public async Task<IActionResult> Login(Logincs logincs)
        {
           
            if (logincs.Email == "guldane@gmail.com" && logincs.PassWord == "123")
            {
                List<Claim> claims = new List<Claim>()
{
new Claim(ClaimTypes.NameIdentifier, logincs.Email),
new Claim("DiğerÖzellikler","Örnek Rol")
};
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
                //Gerekli Taleplerle bir ClaimsIdentity oluşturun ve kullanıcıda oturum açmak için SignInAsync’i çağırın:
                AuthenticationProperties prop = new AuthenticationProperties()
                {
                    AllowRefresh = true, // Refreshing the authentication session should be allowed
                    IsPersistent = logincs.LoggedStatus//Kimlik doğrulama oturumunun birden çok istekte kalıcı olup olmadığını belirlemek içindir.
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), prop);
                return RedirectToAction("Index", "Home");
            }// // Connect the existing harici cookie ve bilgileri yazar
            ViewData["OnayMesaji"] = "Kullanıcı Bulunamadı";
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
