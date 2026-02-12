using arabakiralam.Data;
using Microsoft.AspNetCore.Mvc;

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
    }
}
