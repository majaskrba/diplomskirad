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
    public class LaboratorijasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LaboratorijasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Laboratorijas
        public async Task<IActionResult> Index()
        {
            return View(await _context.laboratorija.ToListAsync());
        }

        // GET: Laboratorijas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratorija = await _context.laboratorija
                .FirstOrDefaultAsync(m => m.LaboratorijaID == id);
            if (laboratorija == null)
            {
                return NotFound();
            }

            return View(laboratorija);
        }

        // GET: Laboratorijas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laboratorijas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LaboratorijaID,Naziv,Lokacija,Email")] Laboratorija laboratorija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laboratorija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(laboratorija);
        }

        // GET: Laboratorijas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratorija = await _context.laboratorija.FindAsync(id);
            if (laboratorija == null)
            {
                return NotFound();
            }
            return View(laboratorija);
        }

        // POST: Laboratorijas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LaboratorijaID,Naziv,Lokacija,Email")] Laboratorija laboratorija)
        {
            if (id != laboratorija.LaboratorijaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laboratorija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaboratorijaExists(laboratorija.LaboratorijaID))
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
            return View(laboratorija);
        }

        // GET: Laboratorijas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratorija = await _context.laboratorija
                .FirstOrDefaultAsync(m => m.LaboratorijaID == id);
            if (laboratorija == null)
            {
                return NotFound();
            }

            return View(laboratorija);
        }

        // POST: Laboratorijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laboratorija = await _context.laboratorija.FindAsync(id);
            _context.laboratorija.Remove(laboratorija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaboratorijaExists(int id)
        {
            return _context.laboratorija.Any(e => e.LaboratorijaID == id);
        }
    }
}
