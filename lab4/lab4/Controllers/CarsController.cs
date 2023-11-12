using Lab02;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class CarsController : Controller
    {
        private readonly Lab1Context _db;

        public CarsController(Lab1Context db)
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
