using Lab02;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class SettlementsController : Controller
    {
        private readonly Lab1Context _db;

        public SettlementsController(Lab1Context db)
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
