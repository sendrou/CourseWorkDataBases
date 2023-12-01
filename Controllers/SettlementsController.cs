using Cargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class SettlementsController : Controller
    {
        private readonly CargoContext _db;

        public SettlementsController(CargoContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int numberRows = 100;
            
           List<Settlement> settlements = _db.Settlements.Take(numberRows).ToList();

            return PartialView(settlements);
        }
    }
}
