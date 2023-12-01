using Cargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class DistancesController : Controller
    {
        private readonly CargoContext _db;

        public DistancesController(CargoContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int numberRows = 100;

            List<Distance> distances = _db.Distances.Include("DeparturesSettlement").Include("ArrivalSettlement").Take(numberRows).ToList();

            return PartialView(distances);
        }
    }
}
