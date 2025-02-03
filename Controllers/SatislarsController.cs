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
    public class SatislarsController : Controller
    {
        private readonly MuzikaaletleristokContext _context;

        public SatislarsController(MuzikaaletleristokContext context)
        {
            _context = context;
        }

        // GET: Satislars
        public async Task<IActionResult> Index()
        {
            var muzikaaletleristokContext = _context.Satislar.Include(s => s.Kullanici).Include(s => s.MuzikAleti);
            return View(await muzikaaletleristokContext.ToListAsync());
        }

        // GET: Satislars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satislar = await _context.Satislar
                .Include(s => s.Kullanici)
                .Include(s => s.MuzikAleti)
                .FirstOrDefaultAsync(m => m.SatisId == id);
            if (satislar == null)
            {
                return NotFound();
            }

            return View(satislar);
        }

        // GET: Satislars/Create
        public IActionResult Create()
        {
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "KullaniciAdi");
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiAdi");
            return View();
        }

        // POST: Satislars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SatisId,MuzikAletiId,KullaniciId,SatisTarihi,Miktar,ToplamFiyat")] Satislar satislar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(satislar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "KullaniciId", satislar.KullaniciId);
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiId", satislar.MuzikAletiId);
            return View(satislar);
        }

        // GET: Satislars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satislar = await _context.Satislar.FindAsync(id);
            if (satislar == null)
            {
                return NotFound();
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "KullaniciAdi", satislar.KullaniciId);
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiAdi", satislar.MuzikAletiId);
            return View(satislar);
        }

        // POST: Satislars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SatisId,MuzikAletiId,KullaniciId,SatisTarihi,Miktar,ToplamFiyat")] Satislar satislar)
        {
            if (id != satislar.SatisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(satislar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SatislarExists(satislar.SatisId))
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
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicilar, "KullaniciId", "KullaniciId", satislar.KullaniciId);
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiId", satislar.MuzikAletiId);
            return View(satislar);
        }

        // GET: Satislars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var satislar = await _context.Satislar
                .Include(s => s.Kullanici)
                .Include(s => s.MuzikAleti)
                .FirstOrDefaultAsync(m => m.SatisId == id);
            if (satislar == null)
            {
                return NotFound();
            }

            return View(satislar);
        }

        // POST: Satislars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var satislar = await _context.Satislar.FindAsync(id);
            if (satislar != null)
            {
                _context.Satislar.Remove(satislar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SatislarExists(int id)
        {
            return _context.Satislar.Any(e => e.SatisId == id);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }
    }
}
