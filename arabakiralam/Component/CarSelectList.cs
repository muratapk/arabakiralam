using arabakiralam.Data;
using Microsoft.AspNetCore.Mvc;

namespace arabakiralam.Component
{
    public class CarSelectList:ViewComponent
    {
        private readonly RentCarDb _db;
        public CarSelectList(RentCarDb db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var marka = _db.Brands.ToList();
            return View(marka);
        }
    }
}
