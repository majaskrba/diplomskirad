using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diplomskirad.Data;
using diplomskirad.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using diplomskirad.Models.ViewModels;

namespace diplomskirad.Controllers
{
    public class TipAnalizesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public TipAnalizesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        // GET: TipAnalizes
        public async Task<IActionResult> Index1(int? id)
        {
            TipAnalize analiza = await _context.tipanalize.FindAsync(id);
            return View(analiza);
        }
      
        // GET: TipAnalizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipAnalize = await _context.tipanalize
                .FirstOrDefaultAsync(m => m.TipAnalizeID == id);
            if (tipAnalize == null)
            {
                return NotFound();
            }

            return View(tipAnalize);
        }

        // GET: TipAnalizes/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create1()
        {
            return View();
        }


        // POST: TipAnalizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipAnalizeID,Naziv,Opis,Slika")] TipAnalize tipAnalize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipAnalize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipAnalize);
        }

        // GET: TipAnalizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipAnalize = await _context.tipanalize.FindAsync(id);
            if (tipAnalize == null)
            {
                return NotFound();
            }
            return View(tipAnalize);
        }

        // POST: TipAnalizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipAnalizeID,Naziv,Opis,Slika")] TipAnalize tipAnalize)
        {
            if (id != tipAnalize.TipAnalizeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipAnalize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipAnalizeExists(tipAnalize.TipAnalizeID))
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
            return View(tipAnalize);
        }
        
         [HttpPost]
        public IActionResult Create1(TipAnalizeViewModels model)
        {
            string stringFileName = UploadFile(model);
            var TipAnalize = new TipAnalize
            {

                
                Slika = stringFileName,
                Naziv=model.Naziv,
                Opis=model.Opis
                
            };
            _context.tipanalize.Add(TipAnalize);//dodavanje u bazu
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        
        private string UploadFile(TipAnalizeViewModels model)
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
        // GET: TipAnalizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipAnalize = await _context.tipanalize
                .FirstOrDefaultAsync(m => m.TipAnalizeID == id);
            if (tipAnalize == null)
            {
                return NotFound();
            }

            return View(tipAnalize);
        }

        // POST: TipAnalizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipAnalize = await _context.tipanalize.FindAsync(id);
            _context.tipanalize.Remove(tipAnalize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipAnalizeExists(int id)
        {
            return _context.tipanalize.Any(e => e.TipAnalizeID == id);
        }
    }
}
