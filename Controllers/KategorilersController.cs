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
    public class KategorilersController : Controller
    {
        private readonly MuzikaaletleristokContext _context;

        public KategorilersController(MuzikaaletleristokContext context)
        {
            _context = context;
        }

        // GET: Kategorilers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kategoriler.ToListAsync());
        }

        // GET: Kategorilers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoriler = await _context.Kategoriler
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (kategoriler == null)
            {
                return NotFound();
            }

            return View(kategoriler);
        }

        // GET: Kategorilers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategorilers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategoriId,KategoriAdi")] Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategoriler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategoriler);
        }

        // GET: Kategorilers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoriler = await _context.Kategoriler.FindAsync(id);
            if (kategoriler == null)
            {
                return NotFound();
            }
            return View(kategoriler);
        }

        // POST: Kategorilers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KategoriId,KategoriAdi")] Kategoriler kategoriler)
        {
            if (id != kategoriler.KategoriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategoriler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategorilerExists(kategoriler.KategoriId))
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
            return View(kategoriler);
        }

        // GET: Kategorilers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoriler = await _context.Kategoriler
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (kategoriler == null)
            {
                return NotFound();
            }

            return View(kategoriler);
        }

        // POST: Kategorilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategoriler = await _context.Kategoriler.FindAsync(id);
            if (kategoriler != null)
            {
                _context.Kategoriler.Remove(kategoriler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategorilerExists(int id)
        {
            return _context.Kategoriler.Any(e => e.KategoriId == id);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }
    }
}
