using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Collections.Generic;
using System.Drawing.Printing;
using static System.Net.Mime.MediaTypeNames;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class CargoTransportationsController : Controller
    {
        private readonly CargoContext _db;

        public CargoTransportationsController(CargoContext db)
        {
            _db = db;
        }
        // GET: /CargoTransportations/Create
        public IActionResult Create()
        {
            List<Car> cars = _db.Cars.ToList();

            List<Distance> distances = _db.Distances.ToList();
            List<Driver> drivers = _db.Drivers.ToList();

            List<Load> loads = _db.Loads.ToList();

            List<Organization> organizations = _db.Organizations.ToList();

            List<TransportationTariff> transportationTariffs = _db.TransportationTariffs.ToList();


            CreateCargoTransportationsViewModel viewModel = new CreateCargoTransportationsViewModel(cars,distances,drivers,loads,organizations,transportationTariffs);

            return View(viewModel);
        }

        // POST: /TransportationTariffs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCargoTransportationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the model entity
                var cargoTransportation = new CargoTransportation
                {
                    CarId = model.CarId,
                    DriverId = model.DriverId,
                    DistanceId = model.DistanceId,
                    LoadId = model.LoadId,
                    OrganizationId = model.OrganizationId,
                    TransportationTariffId = model.TransportationTariffId,
                    Date = model.Date,

                    
                };

                // Save the tariff to the database
                _db.CargoTransportations.Add(cargoTransportation);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        public IActionResult Index()
        {
            int pageSize = 10;
            List<Car> cars = _db.Cars.ToList(); 
            List<Distance> distances = _db.Distances.ToList();
            List<Driver> drivers = _db.Drivers.ToList();
            List<Load> loads = _db.Loads.ToList();
            List<Organization> organizations = _db.Organizations.ToList();
            List<TransportationTariff> transportationTariffs = _db.TransportationTariffs.ToList();
            DateTime date = DateTime.Now;
            cars.Insert(0, new Car { RegistrationNumber = "Все", CarId = 0 });
            distances.Insert(0, new Distance { Distance1 = 0, DistanceId = 0 });
            drivers.Insert(0, new Driver { FullName = "Все", DriverId = 0 });
            loads.Insert(0, new Load { LoadName = "Все", LoadId = 0 });
            organizations.Insert(0, new Organization { OrganizationName = "Все", OrganizationId = 0 });
            transportationTariffs.Insert(0, new TransportationTariff { TariffPerM3Km = 0, TransportationTariffId = 0 });

            List<CargoTransportation> cargoTransportations = _db.CargoTransportations.ToList();

            int car, page;
            if (Request.Cookies.TryGetValue("carNumber", out string carNumber))
            {
                car = Convert.ToInt32(carNumber);
            }
            else
            {
                car = 0;
            }
            if (car != null && car != 0)
            {
                cargoTransportations = cargoTransportations.Where(t => t.CarId == car).ToList();
            }
            if (Request.Cookies.TryGetValue("cargoPage", out string pageString))
            {
                page = Convert.ToInt32(pageString);
            }
            else
            {
                page = 1;
            }
            var items = cargoTransportations.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(cargoTransportations.Count, page, pageSize);
            FilterCargoTransportationsViewModel filterTestsViewModel = new FilterCargoTransportationsViewModel(cars, distances, drivers, loads, organizations, 
                transportationTariffs, 0, 0, 0, 0, 0, 0, date);
            var viewModel = new CargoTransportationsViewModel(items, pageViewModel, filterTestsViewModel);

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            CargoTransportation cargo = _db.CargoTransportations.FirstOrDefault(t => t.DocumentId == id);

            if (cargo == null)
            {
                return NotFound();
            }

            List<Car> cars = _db.Cars.ToList();
            List< Distance > distances = _db.Distances.ToList();
            List<Driver> drivers = _db.Drivers.ToList();
            List< Load > loads = _db.Loads.ToList();
            List<Organization> organizations  = _db.Organizations.ToList();
            List< TransportationTariff > transportationTariffs = _db.TransportationTariffs.ToList();

            EditCargoTransportationsViewModel model = new EditCargoTransportationsViewModel(cars,  distances, drivers, loads,  organizations,  transportationTariffs, cargo.CarId, cargo.DistanceId, cargo.DriverId,cargo.LoadId, cargo.OrganizationId, cargo.TransportationTariffId, cargo.Date);

            model.Id = cargo.DocumentId;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCargoTransportationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                CargoTransportation cargo = _db.CargoTransportations.FirstOrDefault(t => t.DocumentId == model.Id);

                if (cargo != null)
                {
                   
                    cargo.LoadId = model.SelectedLoadId;
                    cargo.CarId = model.SelectedCarId;
                    cargo.OrganizationId = model.SelectedOrganizationId;
                    cargo.TransportationTariffId = model.SelectedTransportationTariffId;
                    cargo.DriverId = model.SelectedDriverId;
                    cargo.Date = model.SelectedDate;
                    cargo.DistanceId = model.SelectedDistanceId;


                    _db.SaveChanges();


                    return RedirectToAction("Index");

                }
            }
            List<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();



            model.Cars = new SelectList(_db.Cars.ToList(), "CarId", "RegistrationNumber");

            model.Distances = new SelectList(_db.Distances.ToList(), "DistanceId", "Distance1");

            model.Drivers = new SelectList(_db.Drivers.ToList(), "DriverId", "FullName");

            model.Loads = new SelectList(_db.Loads.ToList(), "LoadId", "LoadName");
            model.Organizations = new SelectList(_db.Organizations.ToList(), "OrganizationId", "OrganizationName");

            model.TransportationTariffs = new SelectList(_db.TransportationTariffs.ToList(), "TransportationTariffId", "TariffPerTKm");
    
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIndex(int car = 0, int page = 1)
        {
            Response.Cookies.Delete("CarNumber");
            Response.Cookies.Append("CarNumber", car.ToString());


            Response.Cookies.Delete("CargoPage");
            Response.Cookies.Append("CargoPage", page.ToString());




            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            CargoTransportation cargoTransportation = _db.CargoTransportations.FirstOrDefault(t => t.DocumentId == id);

            if (cargoTransportation != null)
            {
                _db.CargoTransportations.Remove(cargoTransportation);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
