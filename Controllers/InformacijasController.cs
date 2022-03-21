using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diplomskirad.Data;
using diplomskirad.Models;
using diplomskirad.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace diplomskirad.Controllers
{
    public class InformacijasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public InformacijasController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        // GET: Informacijas
        public async Task<IActionResult> Index()
        {
            return View(await _context.informacija.ToListAsync());
        }

        // GET: Informacijas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacija = await _context.informacija
                .FirstOrDefaultAsync(m => m.InformacijaID == id);
            if (informacija == null)
            {
                return NotFound();
            }

            return View(informacija);
        }

        // GET: Informacijas/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create1()
        {
            return View();

        }


        // POST: Informacijas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InformacijaID,Naslov,Opis,DatumDogadjaja,Od,Do,DatumIVrijemeObjave,Autor,Slika")] Informacija informacija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informacija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(informacija);
        }
        [HttpPost]
        public IActionResult Create1(InformacijaViewModels model)
        {
            string stringFileName = UploadFile(model);
            var Informacija = new Informacija
            {

                Naslov = model.Naslov,
                Opis = model.Opis,
                DatumDogadjaja = model.DatumDogadjaja,
                Od=model.Od,
                Do=model.Do,
                DatumIVrijemeObjave=model.DatumIVrijemeObjave,
                Autor = model.Autor,
                Slika = stringFileName
            };
            _context.informacija.Add(Informacija);//dodavanje u bazu
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        private string UploadFile(InformacijaViewModels model)
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

        // GET: Informacijas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacija = await _context.informacija.FindAsync(id);
            if (informacija == null)
            {
                return NotFound();
            }
            return View(informacija);
        }

        // POST: Informacijas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InformacijaID,Naslov,Opis,DatumDogadjaja,Od,Do,DatumIVrijemeObjave,Autor,Slika")] Informacija informacija)
        {
            if (id != informacija.InformacijaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informacija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformacijaExists(informacija.InformacijaID))
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
            return View(informacija);
        }

        // GET: Informacijas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var informacija = await _context.informacija
                .FirstOrDefaultAsync(m => m.InformacijaID == id);
            if (informacija == null)
            {
                return NotFound();
            }

            return View(informacija);
        }

        // POST: Informacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var informacija = await _context.informacija.FindAsync(id);
            _context.informacija.Remove(informacija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformacijaExists(int id)
        {
            return _context.informacija.Any(e => e.InformacijaID == id);
        }
    }
}
