using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using arabakiralam.Data;
using arabakiralam.Models;
using arabakiralam.Services;

namespace arabakiralam.Controllers
{
    public class CarImagesController : Controller
    {
        private readonly RentCarDb _context;

        public CarImagesController(RentCarDb context)
        {
            _context = context;
        }

        // GET: CarImages
        public async Task<IActionResult> Index()
        {
            var rentCarDb = _context.CarImages.Include(c => c.Car);
            return View(await rentCarDb.ToListAsync());
        }

        // GET: CarImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carImages = await _context.CarImages
                .Include(c => c.Car)
                .FirstOrDefaultAsync(m => m.CarImagesId == id);
            if (carImages == null)
            {
                return NotFound();
            }

            return View(carImages);
        }

        // GET: CarImages/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "PlateNumber");
            return View();
        }

        // POST: CarImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarImagesId,CarImagesName,CarImagesUrl,CarId")] CarImages carImages,List<IFormFile> ImageFile)
        {

            //FileService dosya = new FileService();
            //string yol = dosya.UploadingFile(ImageFile);
            //carImages.CarImagesUrl = yol;
            foreach (var image in ImageFile)
            {
                var filename=Guid.NewGuid().ToString()+Path.GetExtension(image.FileName);
                var filepath = Path.Combine("wwwroot/Images/", filename);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                CarImages araba=new CarImages();
                araba.CarId = carImages.CarId;
                araba.CarImagesName = carImages.CarImagesName;
                araba.CarImagesUrl = "/Images/" + filename;
                _context.CarImages.Add(araba);
            }

            if (ModelState.IsValid)
            {
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "PlateNumber", carImages.CarId);
            return View(carImages);
        }

        // GET: CarImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carImages = await _context.CarImages.FindAsync(id);
            if (carImages == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "PlateNumber", carImages.CarId);
            return View(carImages);
        }

        // POST: CarImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarImagesId,CarImagesName,CarImagesUrl,CarId")] CarImages carImages,IFormFile ImageFile)
        {
            if (id != carImages.CarImagesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carImages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarImagesExists(carImages.CarImagesId))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", carImages.CarId);
            return View(carImages);
        }

        // GET: CarImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carImages = await _context.CarImages
                .Include(c => c.Car)
                .FirstOrDefaultAsync(m => m.CarImagesId == id);
            if (carImages == null)
            {
                return NotFound();
            }

            return View(carImages);
        }

        // POST: CarImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carImages = await _context.CarImages.FindAsync(id);
            if (carImages != null)
            {
                _context.CarImages.Remove(carImages);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarImagesExists(int id)
        {
            return _context.CarImages.Any(e => e.CarImagesId == id);
        }
    }
}
