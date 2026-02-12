using arabakiralam.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace arabakiralam.Component
{
    public class RezervationFormVC:ViewComponent
    {
        private readonly RentCarDb _context;
        public RezervationFormVC(RentCarDb context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.CarCategory = new SelectList(_context.CarCategories, "CarCategoryId", "CategoryName");
            ViewBag.Car = new SelectList(_context.Cars, "CarId", "Model");
            ViewBag.Brand = new SelectList(_context.Brands, "BrandId", "Name");
            return View();
        }
    }
}
