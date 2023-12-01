using Cargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class CarsController : Controller
    {
        private readonly CargoContext _db;

        public CarsController(CargoContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int numberRows = 100;

            List<Car> cars = _db.Cars.Include("CarBrand").Take(numberRows).ToList();

       

            return PartialView(cars);
        }
    }
}
