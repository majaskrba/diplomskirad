using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diplomskirad.Data;
using diplomskirad.Models;

namespace diplomskirad.Controllers
{
    public class KomentarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KomentarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Komentars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.komentar.Include(k => k.Informacija);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Komentars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.komentar
                .Include(k => k.Informacija)
                .FirstOrDefaultAsync(m => m.KomentarID == id);
            if (komentar == null)
            {
                return NotFound();
            }

            return View(komentar);
        }

        // GET: Komentars/Create
        public IActionResult Create()
        {
            ViewData["InformacijaID"] = new SelectList(_context.informacija, "InformacijaID", "Naslov");
            return View();
        }

        // POST: Komentars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KomentarID,Sadrzaj,Autor,DatumIVrijemeObjave,InformacijaID")] Komentar komentar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(komentar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InformacijaID"] = new SelectList(_context.informacija, "InformacijaID", "Naslov", komentar.InformacijaID);
            return View(komentar);
        }

        // GET: Komentars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.komentar.FindAsync(id);
            if (komentar == null)
            {
                return NotFound();
            }
            ViewData["InformacijaID"] = new SelectList(_context.informacija, "InformacijaID", "Naslov", komentar.InformacijaID);
            return View(komentar);
        }

        // POST: Komentars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KomentarID,Sadrzaj,Autor,DatumIVrijemeObjave,InformacijaID")] Komentar komentar)
        {
            if (id != komentar.KomentarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(komentar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KomentarExists(komentar.KomentarID))
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
            ViewData["InformacijaID"] = new SelectList(_context.informacija, "InformacijaID", "Naslov", komentar.InformacijaID);
            return View(komentar);
        }

        // GET: Komentars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.komentar
                .Include(k => k.Informacija)
                .FirstOrDefaultAsync(m => m.KomentarID == id);
            if (komentar == null)
            {
                return NotFound();
            }

            return View(komentar);
        }

        // POST: Komentars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var komentar = await _context.komentar.FindAsync(id);
            _context.komentar.Remove(komentar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KomentarExists(int id)
        {
            return _context.komentar.Any(e => e.KomentarID == id);
        }
    }
}
