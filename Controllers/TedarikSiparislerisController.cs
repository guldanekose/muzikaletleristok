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
    public class TedarikSiparislerisController : Controller
    {
        private readonly MuzikaaletleristokContext _context;

        public TedarikSiparislerisController(MuzikaaletleristokContext context)
        {
            _context = context;
        }

        // GET: TedarikSiparisleris
        public async Task<IActionResult> Index()
        {
            var muzikaaletleristokContext = _context.TedarikSiparisleri.Include(t => t.MuzikAleti).Include(t => t.Tedarikci);
            return View(await muzikaaletleristokContext.ToListAsync());
        }

        // GET: TedarikSiparisleris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedarikSiparisleri = await _context.TedarikSiparisleri
                .Include(t => t.MuzikAleti)
                .Include(t => t.Tedarikci)
                .FirstOrDefaultAsync(m => m.TedarikSiparisId == id);
            if (tedarikSiparisleri == null)
            {
                return NotFound();
            }

            return View(tedarikSiparisleri);
        }

        // GET: TedarikSiparisleris/Create
        public IActionResult Create()
        {
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiAdi");
            ViewData["TedarikciId"] = new SelectList(_context.Tedarikciler, "TedarikciId", "TedarikciAdi");
            return View();
        }

        // POST: TedarikSiparisleris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TedarikSiparisId,TedarikciId,MuzikAletiId,SiparisTarihi,Miktar,ToplamFiyat")] TedarikSiparisleri tedarikSiparisleri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tedarikSiparisleri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiId", tedarikSiparisleri.MuzikAletiId);
            ViewData["TedarikciId"] = new SelectList(_context.Tedarikciler, "TedarikciId", "TedarikciId", tedarikSiparisleri.TedarikciId);
            return View(tedarikSiparisleri);
        }

        // GET: TedarikSiparisleris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedarikSiparisleri = await _context.TedarikSiparisleri.FindAsync(id);
            if (tedarikSiparisleri == null)
            {
                return NotFound();
            }
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiAdi", tedarikSiparisleri.MuzikAletiId);
            ViewData["TedarikciId"] = new SelectList(_context.Tedarikciler, "TedarikciId", "TedarikciAdi", tedarikSiparisleri.TedarikciId);
            return View(tedarikSiparisleri);
        }

        // POST: TedarikSiparisleris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TedarikSiparisId,TedarikciId,MuzikAletiId,SiparisTarihi,Miktar,ToplamFiyat")] TedarikSiparisleri tedarikSiparisleri)
        {
            if (id != tedarikSiparisleri.TedarikSiparisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tedarikSiparisleri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TedarikSiparisleriExists(tedarikSiparisleri.TedarikSiparisId))
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
            ViewData["MuzikAletiId"] = new SelectList(_context.MuzikAletleri, "MuzikAletiId", "MuzikAletiId", tedarikSiparisleri.MuzikAletiId);
            ViewData["TedarikciId"] = new SelectList(_context.Tedarikciler, "TedarikciId", "TedarikciId", tedarikSiparisleri.TedarikciId);
            return View(tedarikSiparisleri);
        }

        // GET: TedarikSiparisleris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedarikSiparisleri = await _context.TedarikSiparisleri
                .Include(t => t.MuzikAleti)
                .Include(t => t.Tedarikci)
                .FirstOrDefaultAsync(m => m.TedarikSiparisId == id);
            if (tedarikSiparisleri == null)
            {
                return NotFound();
            }

            return View(tedarikSiparisleri);
        }

        // POST: TedarikSiparisleris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tedarikSiparisleri = await _context.TedarikSiparisleri.FindAsync(id);
            if (tedarikSiparisleri != null)
            {
                _context.TedarikSiparisleri.Remove(tedarikSiparisleri);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TedarikSiparisleriExists(int id)
        {
            return _context.TedarikSiparisleri.Any(e => e.TedarikSiparisId == id);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }
    }
}
