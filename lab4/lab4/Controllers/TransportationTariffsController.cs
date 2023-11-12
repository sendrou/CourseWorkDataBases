using Lab02;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab04.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class TransportationTariffsController : Controller
    {
        private readonly Lab1Context _db;

        public TransportationTariffsController(Lab1Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int numberRows = 100;

            List<TransportationTariff> tariff = _db.TransportationTariffs.Take(numberRows).ToList();

            return PartialView(tariff);
        }
    }
}
