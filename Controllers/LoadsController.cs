using Cargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class LoadsController : Controller
    {

        private readonly CargoContext _db;

        public LoadsController(CargoContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int numberRows = 100;

            List<Load> loads = _db.Loads.Take(numberRows).ToList();

            return PartialView(loads);
        }
    }
}
