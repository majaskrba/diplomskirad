using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diplomskirad.Data;
using diplomskirad.Models;

namespace konacno.Controllers
{
    public class SifarniksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SifarniksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sifarniks
        public async Task<IActionResult> Index()
        {
            return View(await _context.sifarnik.ToListAsync());
        }

        // GET: Sifarniks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sifarnik = await _context.sifarnik
                .FirstOrDefaultAsync(m => m.SifarnikID == id);
            if (sifarnik == null)
            {
                return NotFound();
            }

            return View(sifarnik);
        }

        // GET: Sifarniks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sifarniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SifarnikID,Naziv,Min,Max,Jedinica")] Sifarnik sifarnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sifarnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sifarnik);
        }

        // GET: Sifarniks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sifarnik = await _context.sifarnik.FindAsync(id);
            if (sifarnik == null)
            {
                return NotFound();
            }
            return View(sifarnik);
        }

        // POST: Sifarniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SifarnikID,Naziv,Min,Max,Jedinica")] Sifarnik sifarnik)
        {
            if (id != sifarnik.SifarnikID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sifarnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SifarnikExists(sifarnik.SifarnikID))
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
            return View(sifarnik);
        }

        // GET: Sifarniks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sifarnik = await _context.sifarnik
                .FirstOrDefaultAsync(m => m.SifarnikID == id);
            if (sifarnik == null)
            {
                return NotFound();
            }

            return View(sifarnik);
        }

        // POST: Sifarniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sifarnik = await _context.sifarnik.FindAsync(id);
            _context.sifarnik.Remove(sifarnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SifarnikExists(int id)
        {
            return _context.sifarnik.Any(e => e.SifarnikID == id);
        }
    }
}
