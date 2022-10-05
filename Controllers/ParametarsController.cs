using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diplomskirad.Data;
using diplomskirad.Models;
using Microsoft.AspNetCore.Http;

namespace konacno.Controllers
{
    public class ParametarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParametarsController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        // GET: Parametars
        public async Task<IActionResult> Index()
        {



              var nalaz = _context.nalaz.FirstOrDefault(p => p.Email == User.Identity.Name);
               var parameters = await _context.parametar.Include(p => p.Sifarnik).Include(p => p.Nalaz).Where(p => p.Nalaz.Email == User.Identity.Name).Where(p => p.NalazID == nalaz.NalazID).ToListAsync();
              
         //  var parameters = _context.parametar.Include(p => p.Sifarnik).Include(p => p.Nalaz).Where(p => p.Nalaz.Email == User.Identity.Name).Where(p => p.NalazID == id);
       //     var nalaz = _context.nalaz.FirstOrDefault(p => p.Email == User.Identity.Name);



            ViewBag.ime = nalaz.Imeiprezime;
            ViewBag.email = nalaz.Email;
            ViewBag.brojtel = nalaz.Brojtelefona;
            ViewBag.datum = nalaz.Datum;
            ViewBag.vrijeme = nalaz.Vrijeme;
            return View(parameters);
           
        }
        // GET: Parametars
        public async Task<IActionResult> Index2(int? id)
        {



              var parameters = _context.parametar.Include(p => p.Sifarnik).Include(p => p.Nalaz).Where(p => p.Nalaz.Email == User.Identity.Name).Where(p => p.NalazID == id);
               var nalaz = _context.nalaz.FirstOrDefault(p => p.Email == User.Identity.Name);

   

            ViewBag.ime = nalaz.Imeiprezime;
            ViewBag.email = nalaz.Email;
            ViewBag.brojtel = nalaz.Brojtelefona;
            ViewBag.datum = nalaz.Datum;
            ViewBag.vrijeme = nalaz.Vrijeme;
            return View(parameters);

        }

        public async Task<IActionResult> Index1(int? id)
        {

            var applicationDbContext = _context.parametar.Include(p => p.Sifarnik).Include(p => p.Nalaz).Where(p => p.NalazID == id);
            var nalaz = _context.nalaz.FirstOrDefault(p => p.NalazID==id);

            ViewBag.ime = nalaz.Imeiprezime;
            ViewBag.email = nalaz.Email;
            ViewBag.brojtel = nalaz.Brojtelefona;
            ViewBag.datum = nalaz.Datum;
            ViewBag.vrijeme = nalaz.Vrijeme;
            return View("Index",await applicationDbContext.ToListAsync());

        }
      

        // GET: Parametars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametar = await _context.parametar
                .Include(p => p.Nalaz)
                .Include(p => p.Sifarnik)
                .FirstOrDefaultAsync(m => m.ParametarID == id);
            if (parametar == null)
            {
                return NotFound();
            }

            return View(parametar);
        }

        // GET: Parametars/Create
        public IActionResult Create()
        {
            ViewData["NalazID"] = new SelectList(_context.nalaz, "NalazID", "NalazID");
            ViewData["SifarnikID"] = new SelectList(_context.sifarnik, "SifarnikID", "Naziv");
            
            return View();
        }

        // POST: Parametars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParametarID,Vrijednost,NalazID,SifarnikID")] Parametar parametar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parametar);
                await _context.SaveChangesAsync();
               // return RedirectToAction(nameof(Index));
                return RedirectToAction("Index1", new { id = parametar.NalazID });

            }
            ViewData["NalazID"] = new SelectList(_context.nalaz, "NalazID", "NalazID", parametar.NalazID);
            ViewData["SifarnikID"] = new SelectList(_context.sifarnik, "SifarnikID", "Naziv", parametar.SifarnikID);
            // return View("Index");
             
            return View(parametar);
        }

        // GET: Parametars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametar = await _context.parametar.FindAsync(id);
            if (parametar == null)
            {
                return NotFound();
            }
            ViewData["NalazID"] = new SelectList(_context.nalaz, "NalazID", "NalazID", parametar.NalazID);
            ViewData["SifarnikID"] = new SelectList(_context.sifarnik, "SifarnikID", "Naziv", parametar.SifarnikID);
            return View(parametar);
        }

        // POST: Parametars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParametarID,Vrijednost,NalazID,SifarnikID")] Parametar parametar)
        {
            if (id != parametar.ParametarID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parametar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParametarExists(parametar.ParametarID))
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
            ViewData["NalazID"] = new SelectList(_context.nalaz, "NalazID", "NalazID", parametar.NalazID);
            ViewData["SifarnikID"] = new SelectList(_context.sifarnik, "SifarnikID", "Naziv", parametar.SifarnikID);
            return View(parametar);
        }

        // GET: Parametars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametar = await _context.parametar
                .Include(p => p.Nalaz)
                .Include(p => p.Sifarnik)
                .FirstOrDefaultAsync(m => m.ParametarID == id);
            if (parametar == null)
            {
                return NotFound();
            }

            return View(parametar);
        }

        // POST: Parametars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parametar = await _context.parametar.FindAsync(id);
            _context.parametar.Remove(parametar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParametarExists(int id)
        {
            return _context.parametar.Any(e => e.ParametarID == id);
        }
    }
}
