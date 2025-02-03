using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using muzikaletleristok.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Metadata;

namespace muzikaletleristok.Controllers
{
    [Authorize]//3.adım:
    public class DemoController : Controller
    {
        private readonly MuzikaaletleristokContext _context;
        Cascade cd = new Cascade();
        public DemoController(MuzikaaletleristokContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Kategoriler> kategorilerlist = new List<Kategoriler>();
            cd.KategorilerList = new SelectList(_context.Kategoriler, "KategoriId", "KategoriAdi");
            cd.MuzikAletleriList = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiAdi");
            return View(cd);
        }
        public JsonResult GetMuzikaletleri(int KategoriId)
        {

            var muzikaletlerilist = (from muzikaletleri in _context.MuzikAletleri
                                 where muzikaletleri.KategoriId == KategoriId
                                 select new

                                 {

                                     Text = muzikaletleri.MuzikAletiAdi,

                                     Value = muzikaletleri.MuzikAletiId

                                 }).ToList();

            return Json(muzikaletlerilist, new System.Text.Json.JsonSerializerOptions());
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }
    }
}
