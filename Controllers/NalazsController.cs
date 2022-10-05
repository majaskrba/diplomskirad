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
    public class NalazsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NalazsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nalazs
        public async Task<IActionResult> Index()
        {
            var _aplicapplicationDbContext = _context.nalaz.Where(n => n.Email == User.Identity.Name);
            return View(await _aplicapplicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index1()
        {
         
            return View(await _context.nalaz.ToListAsync());
        }



        public async Task<IActionResult> Pretraga1(string searchString)
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                var nalaz = _context.nalaz.Where(p => p.Email.Contains(searchString));
                if (nalaz != null && (nalaz as IEnumerable<object>).Any())
                {
                    return View("Index1", await nalaz.ToListAsync());
                }
                else
                {
                    ModelState.AddModelError("Error", "Osoba nije pronadjena");
                    var applicationDbContext = _context.nalaz;
                    return View("Index1", await applicationDbContext.ToListAsync());
                }


            }
            else
            {
                //ModelState.AddModelError("Error", "Check ID");
                var applicationDbContext = _context.nalaz;
                return View("Index1", await applicationDbContext.ToListAsync());


            }
        }

        // GET: Nalazs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nalaz = await _context.nalaz
                .FirstOrDefaultAsync(m => m.NalazID == id);
            if (nalaz == null)
            {
                return NotFound();
            }

            return View(nalaz);
        }

        // GET: Nalazs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nalazs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NalazID,Opis,Autor,Email,Imeiprezime,Datum,Vrijeme,Brojtelefona")] Nalaz nalaz)
        {
            if (ModelState.IsValid)
            {
                nalaz.Autor = User.Identity.Name;
                _context.Add(nalaz);
                await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index1));
            }
            return View(nalaz);
        }

        // GET: Nalazs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nalaz = await _context.nalaz.FindAsync(id);
            if (nalaz == null)
            {
                return NotFound();
            }
            return View(nalaz);
        }

        // POST: Nalazs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NalazID,Opis,Autor,Email,Imeiprezime,Datum,Vrijeme,Brojtelefona")] Nalaz nalaz)
        {
            if (id != nalaz.NalazID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nalaz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NalazExists(nalaz.NalazID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index1));
            }
            return View(nalaz);
        }

        // GET: Nalazs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nalaz = await _context.nalaz
                .FirstOrDefaultAsync(m => m.NalazID == id);
            if (nalaz == null)
            {
                return NotFound();
            }

            return View(nalaz);
        }

        // POST: Nalazs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nalaz = await _context.nalaz.FindAsync(id);
            _context.nalaz.Remove(nalaz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index1));
        }

        private bool NalazExists(int id)
        {
            return _context.nalaz.Any(e => e.NalazID == id);
        }
    }
}
