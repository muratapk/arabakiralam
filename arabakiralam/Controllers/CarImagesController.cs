using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using arabakiralam.Data;
using arabakiralam.Models;

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
        public async Task<IActionResult> Create([Bind("CarImagesId,CarImagesName,CarImagesUrl,CarId")] CarImages carImages,IFormFile ImageFile)
        {
            long maxFilesize = 5 * 1024 * 1024;
            string[] AllowTypesExtension = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            if(ImageFile!=null && ImageFile.Length>0)
            {
                if(ImageFile.Length>maxFilesize)
                {
                    ModelState.AddModelError("ImageFile", "Belirtilen dosya 5Mb'dan Büyük Olmaz");
                    return View();
                }
                if(!AllowTypesExtension.Contains(Path.GetExtension(ImageFile.FileName).ToLower()))
                {
                    ModelState.AddModelError("ImageFile", "Dosya Tipi Jpg png gif jpeg olmalıdır");
                    return View();
                }
                var newName=Guid.NewGuid().ToString()+Path.GetExtension(ImageFile.FileName).ToLower();
                var filepath = Path.Combine("wwwroot/Images", newName);
                using(var stream=new FileStream(filepath,FileMode.Create))
                {
                   await ImageFile.CopyToAsync(stream);
                }
                carImages.CarImagesUrl = "/Images/"+newName;

            }

            if (ModelState.IsValid)
            {
                _context.Add(carImages);
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
