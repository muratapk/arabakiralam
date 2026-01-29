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
    public class CarCategoriesController : Controller
    {
        private readonly RentCarDb _context;

        public CarCategoriesController(RentCarDb context)
        {
            _context = context;
        }

        // GET: CarCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarCategories.ToListAsync());
        }

        // GET: CarCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCategory = await _context.CarCategories
                .FirstOrDefaultAsync(m => m.CarCategoryId == id);
            if (carCategory == null)
            {
                return NotFound();
            }

            return View(carCategory);
        }

        // GET: CarCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarCategoryId,CategoryName")] CarCategory carCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carCategory);
        }

        // GET: CarCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCategory = await _context.CarCategories.FindAsync(id);
            if (carCategory == null)
            {
                return NotFound();
            }
            return View(carCategory);
        }

        // POST: CarCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarCategoryId,CategoryName")] CarCategory carCategory)
        {
            if (id != carCategory.CarCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarCategoryExists(carCategory.CarCategoryId))
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
            return View(carCategory);
        }

        // GET: CarCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carCategory = await _context.CarCategories
                .FirstOrDefaultAsync(m => m.CarCategoryId == id);
            if (carCategory == null)
            {
                return NotFound();
            }

            return View(carCategory);
        }

        // POST: CarCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carCategory = await _context.CarCategories.FindAsync(id);
            if (carCategory != null)
            {
                _context.CarCategories.Remove(carCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarCategoryExists(int id)
        {
            return _context.CarCategories.Any(e => e.CarCategoryId == id);
        }
    }
}
