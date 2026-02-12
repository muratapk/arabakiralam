using arabakiralam.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Cryptography;

namespace arabakiralam.Controllers
{
    public class CarDetailsController : Controller
    {
        private readonly RentCarDb _db;
        public CarDetailsController(RentCarDb db)
        {
            _db = db; 
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CarList(int CarCategory,int Brand,string Search,int Car)
        {
            //var car_result=_db.Cars.Where(x=>x.CarCategoryId == CarCategory).ToList();
            //var cars = _db.Cars.Include(x=>x.CarImages).AsQueryable();

            var cars = _db.Cars.Include(x => x.CarImages).AsQueryable();

            if (CarCategory != 0)
            {
                 cars=cars.Where(x=>x.CarCategoryId == CarCategory);
            }
            if (Brand != 0)
            {
                 cars=cars.Where(x=>x.BrandId == Brand);
            }
            if(!string.IsNullOrEmpty(Search))
            {
                 cars=cars.Where(x=>x.Model.Contains(Search));
            }
            if (Car != 0)
            {
                cars=cars.Where(x=>x.CarId == Car);
            }
            return View(cars.ToList());
        }
    }
}
