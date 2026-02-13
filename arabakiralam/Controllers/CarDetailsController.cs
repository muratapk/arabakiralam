using arabakiralam.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Cryptography;
using Newtonsoft.Json;
using arabakiralam.Models;

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
        public IActionResult BrandList(int id)
        {
            var araba=_db.Cars.Include(x=>x.CarImages).Where(x=>x.BrandId == id).ToList();
            return PartialView("_BrandListPartial", araba);
        }
        public IActionResult AddToCart(int id)
        {
            var car = _db.Cars.FirstOrDefault(x => x.CarId == id);
            List<Car> cart;
            //cart isimli içinde araba bilgilerinin olduğu bir liste oluştur
            var sessionCart = HttpContext.Session.GetString("Cart");
            //car isimli session kayıtlı bilgiler varsa çek ve sessionCart ata
            if (sessionCart != null)
            {
                cart=JsonConvert.DeserializeObject<List<Car>>(sessionCart);
                //cart içinde bilgiler varsa json formatından list olarak çıkar ve cart ata

            }
            else
            {
                cart=new List<Car>();
            }
            cart.Add(car);

            HttpContext.Session.SetString("Cart",JsonConvert.SerializeObject(cart));
            return Json("Ürün Sepete Eklendi");

        }

        public IActionResult AddToList()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            //sesion içindeki Cart isimli araba listesin getir
            if (sessionCart == null)
            {
                return View(new List<Car>());
            }
            var cart = JsonConvert.DeserializeObject<List<Car>>(sessionCart);

            return View(cart);
        }
    }
}
