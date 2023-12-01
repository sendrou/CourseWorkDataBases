using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class TransportationTariffsController : Controller
    {
        private readonly CargoContext _db;

        public TransportationTariffsController(CargoContext db)
        {
            _db = db;
        }

        // GET: /TransportationTariffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /TransportationTariffs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateTransportationTariffsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the model entity
                var tariff = new TransportationTariff
                {
                    TariffPerTKm = model.TariffPerTKm,
                    TariffPerM3Km = model.TariffPerM3Km
                };

                // Save the tariff to the database
                _db.TransportationTariffs.Add(tariff);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Index()
        {
            int numberRows = 10;
            List<TransportationTariff> tariffs = _db.TransportationTariffs.Take(numberRows).ToList();
            int count = tariffs.Count;
            PageViewModel pageViewModel = new PageViewModel(count, 1, 10);
            var viewModel = new TransportationTariffsViewModel(tariffs, pageViewModel);

            return View(viewModel);
        }
    }
}