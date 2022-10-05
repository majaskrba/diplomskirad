using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diplomskirad.Data;
using diplomskirad.Models;
using Microsoft.AspNetCore.Hosting;
using diplomskirad.Models.ViewModels;
using System.IO;

namespace diplomskirad.Controllers
{
    public class AnalizesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHostEnvironment;


        public AnalizesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        // GET: Analizes
        public async Task<IActionResult> Index(int? id)
        {
            var applicationDbContext = _context.analize.Include(a => a.TipAnalize).Where(a => a.TipAnalizeID == id);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index1(int? id)
        {
            var applicationDbContext = _context.analize.Include(a => a.TipAnalize).Where(a => a.TipAnalizeID == id);
            return View(await applicationDbContext.ToListAsync());
        }





        public ActionResult GetList()
        {
            var Analize = (from analize in _context.analize select analize).ToList();
            return View("Cjenovnik", Analize);
        }

        [HttpPost]
        public ActionResult GetList(Analize model)
        {
            var data = _context.analize.ToList();
            return View("Cjenovnik", data);


        }




        public async Task<IActionResult> Pretraga(string searchString)
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                var analiza = _context.analize.Where(p =>p.Naziv.Contains (searchString));
                if (analiza != null && (analiza as IEnumerable<object>).Any())
                {
                    return View("Cjenovnik", await analiza.ToListAsync());
                }
                else
                {
                    ModelState.AddModelError("Error", "Analiza nije pronadjena");
                    var applicationDbContext = _context.analize;
                    return View("Cjenovnik", await applicationDbContext.ToListAsync());
                }


            }
            else
            {
                //ModelState.AddModelError("Error", "Check ID");
                var applicationDbContext = _context.analize;
                return View("Cjenovnik", await applicationDbContext.ToListAsync());


            }
        } 
        /*
        public async Task<IActionResult> Indexkartice(int? id)
        {
            var applicationDbContext = _context.analize.Include(a => a.TipAnalize).Where(a => a.AnalizeID == id) ;
            return View(await applicationDbContext.ToListAsync());
        }*/
        // GET: Analizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analize = await _context.analize
                .Include(a => a.TipAnalize)
                .FirstOrDefaultAsync(m => m.AnalizeID == id);
            if (analize == null)
            {
                return NotFound();
            }

            return View(analize);
        }

        // GET: Analizes/Create
        public IActionResult Create()
        {
            ViewData["TipAnalizeID"] = new SelectList(_context.tipanalize, "TipAnalizeID", "Naziv");
            return View();
        }
        public IActionResult Create1()
        {
            return View();
        }

        // POST: Analizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnalizeID,Naziv,Opis,Slika,Cijena,TipAnalizeID")] Analize analize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(analize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipAnalizeID"] = new SelectList(_context.tipanalize, "TipAnalizeID", "Naziv", analize.TipAnalizeID);
            return View(analize);
        }

        // GET: Analizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analize = await _context.analize.FindAsync(id);
            if (analize == null)
            {
                return NotFound();
            }
            ViewData["TipAnalizeID"] = new SelectList(_context.tipanalize, "TipAnalizeID", "Naziv", analize.TipAnalizeID);
            return View(analize);
        }

        // POST: Analizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnalizeID,Naziv,Opis,Slika,Cijena,TipAnalizeID")] Analize analize)
        {
            if (id != analize.AnalizeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(analize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalizeExists(analize.AnalizeID))
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
            ViewData["TipAnalizeID"] = new SelectList(_context.tipanalize, "TipAnalizeID", "Naziv", analize.TipAnalizeID);
            return View(analize);
        }
        [HttpPost]
        public IActionResult Create1(AnalizeViewModels model)
        {
            string stringFileName = UploadFile(model);
            var Analize = new Analize
            {


                Slika = stringFileName,
                Naziv = model.Naziv,
                Opis = model.Opis,
                Cijena=model.Cijena

            };
            _context.analize.Add(Analize);//dodavanje u bazu
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        private string UploadFile(AnalizeViewModels model)
        {
            string fileName = null;
            if (model.Slika != null)
            {
                string UploadDir = Path.Combine(_WebHostEnvironment.WebRootPath, "Images");//smjesti sliku koju si dodao u folder images
                fileName = Guid.NewGuid().ToString() + "-" + model.Slika.FileName; //kovertovanje u string
                string filePath = Path.Combine(UploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Slika.CopyTo(fileStream);
                }
            }
            return fileName;

        }
        // GET: Analizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analize = await _context.analize
                .Include(a => a.TipAnalize)
                .FirstOrDefaultAsync(m => m.AnalizeID == id);
            if (analize == null)
            {
                return NotFound();
            }

            return View(analize);
        }

        // POST: Analizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var analize = await _context.analize.FindAsync(id);
            _context.analize.Remove(analize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnalizeExists(int id)
        {
            return _context.analize.Any(e => e.AnalizeID == id);
        }
    }
}
