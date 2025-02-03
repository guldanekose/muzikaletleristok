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
    public class StokHareketlerisController : Controller
    {
        private readonly MuzikaaletleristokContext _context;

        public StokHareketlerisController(MuzikaaletleristokContext context)
        {
            _context = context;
        }

        // GET: StokHareketleris
        public async Task<IActionResult> Index()
        {
            var muzikaaletleristokContext = _context.StokHareketleri.Include(s => s.MuzikAleti);
            return View(await muzikaaletleristokContext.ToListAsync());
        }

        // GET: StokHareketleris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stokHareketleri = await _context.StokHareketleri
                .Include(s => s.MuzikAleti)
                .FirstOrDefaultAsync(m => m.StokHareketId == id);
            if (stokHareketleri == null)
            {
                return NotFound();
            }

            return View(stokHareketleri);
        }

        // GET: StokHareketleris/Create
        public IActionResult Create()
        {
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiAdi");
            return View();
        }

        // POST: StokHareketleris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StokHareketId,MuzikAletiId,HareketTipi,Miktar,HareketTarihi")] StokHareketleri stokHareketleri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stokHareketleri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiId", stokHareketleri.MuzikAletiId);
            return View(stokHareketleri);
        }

        // GET: StokHareketleris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stokHareketleri = await _context.StokHareketleri.FindAsync(id);
            if (stokHareketleri == null)
            {
                return NotFound();
            }
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiAdi", stokHareketleri.MuzikAletiId);
            return View(stokHareketleri);
        }

        // POST: StokHareketleris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StokHareketId,MuzikAletiId,HareketTipi,Miktar,HareketTarihi")] StokHareketleri stokHareketleri)
        {
            if (id != stokHareketleri.StokHareketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stokHareketleri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StokHareketleriExists(stokHareketleri.StokHareketId))
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
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiId", stokHareketleri.MuzikAletiId);
            return View(stokHareketleri);
        }

        // GET: StokHareketleris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stokHareketleri = await _context.StokHareketleri
                .Include(s => s.MuzikAleti)
                .FirstOrDefaultAsync(m => m.StokHareketId == id);
            if (stokHareketleri == null)
            {
                return NotFound();
            }

            return View(stokHareketleri);
        }

        // POST: StokHareketleris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stokHareketleri = await _context.StokHareketleri.FindAsync(id);
            if (stokHareketleri != null)
            {
                _context.StokHareketleri.Remove(stokHareketleri);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StokHareketleriExists(int id)
        {
            return _context.StokHareketleri.Any(e => e.StokHareketId == id);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }
    }
}
