using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diplomskirad.Data;
using diplomskirad.Models;
using Microsoft.AspNetCore.Authorization;

namespace diplomskirad.Controllers
{
    public class RezervacijasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervacijasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezervacijas
        public async Task<IActionResult> Index()
        {
           
            var applicationDbContext = _context.rezervacija.Include(r => r.Laboratorija).Include(r => r.TipAnalize);
  
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rezervacijas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.rezervacija
                .Include(r => r.Laboratorija)
                .Include(r => r.TipAnalize)
                .FirstOrDefaultAsync(m => m.RezervacijaID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }
        [Authorize]
        // GET: Rezervacijas/Create
        public IActionResult Create()
        {
            ViewData["LaboratorijaID"] = new SelectList(_context.laboratorija, "LaboratorijaID", "Lokacija");
            ViewData["TipAnalizeID"] = new SelectList(_context.tipanalize, "TipAnalizeID", "Naziv");
     
            return View();
        }

        // POST: Rezervacijas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RezervacijaID,Autor,Email,BrojTelefona,Datum,Vrijeme,TipAnalizeID,LaboratorijaID")] Rezervacija rezervacija)
        {

            if (ModelState.IsValid)//objekat koji kreiram rezervaiju u ovom slucaju
            {

                rezervacija.Email = User.Identity.Name;
                    _context.Add(rezervacija);
                ViewBag.poruka = "Uspjesna rezervacija!";
                await _context.SaveChangesAsync();
                //   return RedirectToAction(nameof(Create));

             
                ViewData["LaboratorijaID"] = new SelectList(_context.laboratorija, "LaboratorijaID", "Lokacija");
                ViewData["TipAnalizeID"] = new SelectList(_context.tipanalize, "TipAnalizeID", "Naziv");
                // ViewData["LaboratorijaID"] = new SelectList(_context.laboratorija, "LaboratorijaID", "Lokacija", rezervacija.LaboratorijaID);

                // ViewData["TipAnalizeID"] = new SelectList(_context.tipanalize, "TipAnalizeID", "Naziv", rezervacija.TipAnalizeID);
                ModelState.Clear();
                return View("Create");
            }
          
            return View(rezervacija);
        }

        // GET: Rezervacijas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.rezervacija.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }
            ViewData["LaboratorijaID"] = new SelectList(_context.laboratorija, "LaboratorijaID", "Lokacija", rezervacija.LaboratorijaID);
            ViewData["TipAnalizeID"] = new SelectList(_context.tipanalize, "TipAnalizeID", "Naziv", rezervacija.TipAnalizeID);
            return View(rezervacija);
        }

        // POST: Rezervacijas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RezervacijaID,Autor,Email,BrojTelefona,Datum,Vrijeme,TipAnalizeID,LaboratorijaID")] Rezervacija rezervacija)
        {
            if (id != rezervacija.RezervacijaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaExists(rezervacija.RezervacijaID))
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
            ViewData["LaboratorijaID"] = new SelectList(_context.laboratorija, "LaboratorijaID", "Lokacija", rezervacija.LaboratorijaID);
            ViewData["TipAnalizeID"] = new SelectList(_context.tipanalize, "TipAnalizeID", "Naziv", rezervacija.TipAnalizeID);
            return View(rezervacija);
        }

        // GET: Rezervacijas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacija = await _context.rezervacija
                .Include(r => r.Laboratorija)
                .Include(r => r.TipAnalize)
                .FirstOrDefaultAsync(m => m.RezervacijaID == id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            return View(rezervacija);
        }

        // POST: Rezervacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacija = await _context.rezervacija.FindAsync(id);
            _context.rezervacija.Remove(rezervacija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijaExists(int id)
        {
            return _context.rezervacija.Any(e => e.RezervacijaID == id);
        }
    }
}
