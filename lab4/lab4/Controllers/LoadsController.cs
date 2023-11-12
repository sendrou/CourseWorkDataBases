using Lab02;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class LoadsController : Controller
    {

        private readonly Lab1Context _db;

        public LoadsController(Lab1Context db)
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
