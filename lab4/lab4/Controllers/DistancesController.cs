using Lab02;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class DistancesController : Controller
    {
        private readonly Lab1Context _db;

        public DistancesController(Lab1Context db)
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
