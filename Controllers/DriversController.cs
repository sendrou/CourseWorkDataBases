using Cargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class DriversController : Controller
    {

        private readonly CargoContext _db;

        public DriversController(CargoContext db)
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
