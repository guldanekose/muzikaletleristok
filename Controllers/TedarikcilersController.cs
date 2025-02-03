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
    public class TedarikcilersController : Controller
    {
        private readonly MuzikaaletleristokContext _context;

        public TedarikcilersController(MuzikaaletleristokContext context)
        {
            _context = context;
        }

        // GET: Tedarikcilers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tedarikciler.ToListAsync());
        }

        // GET: Tedarikcilers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedarikciler = await _context.Tedarikciler
                .FirstOrDefaultAsync(m => m.TedarikciId == id);
            if (tedarikciler == null)
            {
                return NotFound();
            }

            return View(tedarikciler);
        }

        // GET: Tedarikcilers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tedarikcilers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TedarikciId,TedarikciAdi,IletisimBilgileri")] Tedarikciler tedarikciler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tedarikciler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tedarikciler);
        }

        // GET: Tedarikcilers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedarikciler = await _context.Tedarikciler.FindAsync(id);
            if (tedarikciler == null)
            {
                return NotFound();
            }
            return View(tedarikciler);
        }

        // POST: Tedarikcilers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TedarikciId,TedarikciAdi,IletisimBilgileri")] Tedarikciler tedarikciler)
        {
            if (id != tedarikciler.TedarikciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tedarikciler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TedarikcilerExists(tedarikciler.TedarikciId))
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
            return View(tedarikciler);
        }

        // GET: Tedarikcilers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedarikciler = await _context.Tedarikciler
                .FirstOrDefaultAsync(m => m.TedarikciId == id);
            if (tedarikciler == null)
            {
                return NotFound();
            }

            return View(tedarikciler);
        }

        // POST: Tedarikcilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tedarikciler = await _context.Tedarikciler.FindAsync(id);
            if (tedarikciler != null)
            {
                _context.Tedarikciler.Remove(tedarikciler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TedarikcilerExists(int id)
        {
            return _context.Tedarikciler.Any(e => e.TedarikciId == id);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }
    }
}
