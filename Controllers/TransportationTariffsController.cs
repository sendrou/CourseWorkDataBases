using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Edit(int id)
        {
            TransportationTariff tariff = _db.TransportationTariffs.FirstOrDefault(t => t.TransportationTariffId == id);

            if (tariff == null)
            {
                return NotFound();
            }


            EditTransportationTariffsViewModel model = new EditTransportationTariffsViewModel(tariff.TariffPerTKm, tariff.TariffPerM3Km);

            model.Id = tariff.TransportationTariffId;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTransportationTariffsViewModel model)
        {
            if (ModelState.IsValid)
            {
                TransportationTariff tariff = _db.TransportationTariffs.FirstOrDefault(t => t.TransportationTariffId == model.Id);

                if (tariff != null)
                {

                    tariff.TariffPerTKm = model.TariffPerTKm;
                    tariff.TariffPerM3Km = model.TariffPerM3Km;



                    _db.SaveChanges();


                    return RedirectToAction("Index");

                }
            }
            List<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();



            return View(model);
        }

        public IActionResult Index()
        {
            int pageSize = 10;
           
            List<TransportationTariff> transportationTariffs = _db.TransportationTariffs.ToList();
           

            int startTariff, endTariff,page;


            if (Request.Cookies.TryGetValue("StartTariffString", out string startTariffString))
            {
                startTariff = Convert.ToInt32(startTariffString);
            }
            else
            {
                startTariff = 0;
            }

            if (Request.Cookies.TryGetValue("EndTariffString", out string endTariffString))
            {
                endTariff = Convert.ToInt32(endTariffString);
            }
            else
            {
                endTariff = 10000;
            }

            
            if (startTariff != null && startTariff >= 0)
            {
                transportationTariffs = transportationTariffs.Where(t => t.TariffPerTKm >= startTariff).Where(t => t.TariffPerM3Km >= startTariff).ToList();
            }
            if (endTariff >= 0 && startTariff <= endTariff && endTariff != null)
            {
                transportationTariffs = transportationTariffs.Where(t => t.TariffPerTKm <= endTariff).Where(t => t.TariffPerM3Km <= endTariff).ToList();
            }

            

            if (Request.Cookies.TryGetValue("TariffPage", out string pageString))
            {
                page = Convert.ToInt32(pageString);
            }
            else
            {
                page = 1;
            }
            var items = transportationTariffs.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(transportationTariffs.Count, page, pageSize);
            FilterTransportationTariffsViewModel filterTariffViewModel = new FilterTransportationTariffsViewModel(startTariff, endTariff);
            var viewModel = new TransportationTariffsViewModel(items, pageViewModel, filterTariffViewModel);

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIndex(int endTariff = 1000, int startTariff = 0,int page = 1)
        {
            

            Response.Cookies.Delete("StartTariffString");
            Response.Cookies.Append("StartTariffString", startTariff.ToString());
            Response.Cookies.Delete("EndTariffString");
            Response.Cookies.Append("EndTariffString", endTariff.ToString());

            

            Response.Cookies.Delete("TariffPage");
            Response.Cookies.Append("TariffPage", page.ToString());


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            TransportationTariff transportationTariff = _db.TransportationTariffs.FirstOrDefault(t => t.TransportationTariffId == id);

            if (transportationTariff != null)
            {
                _db.TransportationTariffs.Remove(transportationTariff);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}