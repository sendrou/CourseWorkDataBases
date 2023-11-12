using Lab02;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class CargoTransportationsController : Controller
    {
        private readonly Lab1Context _db;

        public CargoTransportationsController(Lab1Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int numberRows = 100;

            List<CargoTransportation> cargoTransportations = _db.CargoTransportations.Include("TransportationTariff").Include("Load").Include("Car").Include("Driver").Include("Organization").Include("Distance").Take(numberRows).ToList();

       

            return PartialView(cargoTransportations);
        }
    }
}
