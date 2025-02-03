using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using muzikaletleristok.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;

namespace muzikaletleristok.Controllers
{
    [Authorize]//3.adım:
    public class KullanicilarsController : Controller
    {
        private readonly MuzikaaletleristokContext _context;

        public KullanicilarsController(MuzikaaletleristokContext context)
        {
            _context = context;
        }

        // GET: Kullanicilars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kullanicilar.ToListAsync());
        }

        // GET: Kullanicilars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullanicilar = await _context.Kullanicilar
                .FirstOrDefaultAsync(m => m.KullaniciId == id);
            if (kullanicilar == null)
            {
                return NotFound();
            }

            return View(kullanicilar);
        }

        // GET: Kullanicilars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kullanicilars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KullaniciId,KullaniciAdi,Sifre,Email")] Kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kullanicilar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kullanicilar);
        }

        // GET: Kullanicilars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullanicilar = await _context.Kullanicilar.FindAsync(id);
            if (kullanicilar == null)
            {
                return NotFound();
            }
            return View(kullanicilar);
        }

        // POST: Kullanicilars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KullaniciId,KullaniciAdi,Sifre,Email")] Kullanicilar kullanicilar)
        {
            if (id != kullanicilar.KullaniciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kullanicilar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KullanicilarExists(kullanicilar.KullaniciId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kullanicilar);
        }

        // GET: Kullanicilars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullanicilar = await _context.Kullanicilar
                .FirstOrDefaultAsync(m => m.KullaniciId == id);
            if (kullanicilar == null)
            {
                return NotFound();
            }

            return View(kullanicilar);
        }

        // POST: Kullanicilars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kullanicilar = await _context.Kullanicilar.FindAsync(id);
            if (kullanicilar != null)
            {
                _context.Kullanicilar.Remove(kullanicilar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KullanicilarExists(int id)
        {
            return _context.Kullanicilar.Any(e => e.KullaniciId == id);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }
    }
}
