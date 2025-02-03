using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public class MuzikAletlerisController : Controller
    {
        private readonly MuzikaaletleristokContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MuzikAletlerisController(MuzikaaletleristokContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: MuzikAletleris
        public async Task<IActionResult> Index()
        {
            var muzikaaletleristokContext = _context.MuzikAletleri.Include(m => m.Kategori);
            return View(await muzikaaletleristokContext.ToListAsync());
        }

        // GET: MuzikAletleris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muzikAletleri = await _context.MuzikAletleri
                .Include(m => m.Kategori)
                .FirstOrDefaultAsync(m => m.MuzikAletiId == id);
            if (muzikAletleri == null)
            {
                return NotFound();
            }

            return View(muzikAletleri);
        }

        // GET: MuzikAletleris/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriAdi");
            return View();
        }

        // POST: MuzikAletleris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MuzikAletiId,KategoriId,MuzikAletiAdi,Marka,Model,Fiyat,StokMiktari,MuzikAletiPhoto,ImageFile")] MuzikAletleri muzikAletleri)
        {
            if (ModelState.IsValid)
            {
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(muzikAletleri.ImageFile.FileName);
                string extension = Path.GetExtension(muzikAletleri.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                muzikAletleri.MuzikAletiPhoto = "~/Contents/" + fileName;
                string path = Path.Combine(wwwrootpath + "/Contents/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await muzikAletleri.ImageFile.CopyToAsync(filestream);
                }
                _context.Add(muzikAletleri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId", muzikAletleri.KategoriId);
            return View(muzikAletleri);
        }

        // GET: MuzikAletleris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muzikAletleri = await _context.MuzikAletleri.FindAsync(id);
            if (muzikAletleri == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriAdi", muzikAletleri.KategoriId);
            return View(muzikAletleri);
        }

        // POST: MuzikAletleris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MuzikAletiId,KategoriId,MuzikAletiAdi,Marka,Model,Fiyat,StokMiktari,MuzikAletiPhoto,ImageFile")] MuzikAletleri muzikAletleri)
        {
            if (id != muzikAletleri.MuzikAletiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwrootpath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(muzikAletleri.ImageFile.FileName);
                string extension = Path.GetExtension(muzikAletleri.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                muzikAletleri.MuzikAletiPhoto = "~/Contents/" + fileName;
                string path = Path.Combine(wwwrootpath + "/Contents/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await muzikAletleri.ImageFile.CopyToAsync(filestream);
                }
                try
                {
                    _context.Update(muzikAletleri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuzikAletleriExists(muzikAletleri.MuzikAletiId))
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
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriId", muzikAletleri.KategoriId);
            return View(muzikAletleri);
        }

        // GET: MuzikAletleris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muzikAletleri = await _context.MuzikAletleri
                .Include(m => m.Kategori)
                .FirstOrDefaultAsync(m => m.MuzikAletiId == id);
            if (muzikAletleri == null)
            {
                return NotFound();
            }

            return View(muzikAletleri);
        }

        // POST: MuzikAletleris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var muzikAletleri = await _context.MuzikAletleri.FindAsync(id);
            if (muzikAletleri != null)
            {
                _context.MuzikAletleri.Remove(muzikAletleri);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuzikAletleriExists(int id)
        {
            return _context.MuzikAletleri.Any(e => e.MuzikAletiId == id);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Startp");
        }
    }
}
