using Lab02;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class DriversController : Controller
    {

        private readonly Lab1Context _db;

        public DriversController(Lab1Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int numberRows = 100;

            List<Driver> drivers = _db.Drivers.Take(numberRows).ToList();

            return PartialView(drivers);
        }
    }
}
