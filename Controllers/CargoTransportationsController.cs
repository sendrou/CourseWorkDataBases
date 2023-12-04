using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
            

            drivers.Insert(0, new Driver { FullName = "Все", DriverId = 0 });
            loads.Insert(0, new Load { LoadName = "Все", LoadId = 0 });
            organizations.Insert(0, new Organization { OrganizationName = "Все", OrganizationId = 0 });


            List<CargoTransportation> cargoTransportations = _db.CargoTransportations.ToList();
            string registrationNumber;
            int car, driver, organization,load,startTariff, endTariff, startDistance,endDistance, page;
            DateTime startDate, endDate;

            if (Request.Cookies.TryGetValue("carNumber", out string carNumber))
            {
                car = Convert.ToInt32(carNumber);
            }
            else
            {
                car = 0;
            }

            if (!Request.Cookies.TryGetValue("RegistrationNumber", out registrationNumber))
            {
                registrationNumber = "";
            }

            if (Request.Cookies.TryGetValue("StartDistanceString", out string startDistanceString))
            {
                startDistance = Convert.ToInt32(startDistanceString);
            }
            else
            {
                startDistance = 0;
            }

            if (Request.Cookies.TryGetValue("EndDistanceString", out string endDistanceString))
            {
                endDistance = Convert.ToInt32(endDistanceString);
            }
            else
            {
                endDistance = 10000;
            }

            if (Request.Cookies.TryGetValue("StartDateString", out string startDateString))
            {
                startDate = DateTime.Parse(startDateString);
            }
            else
            {
                startDate = default(DateTime);
            }

            if (Request.Cookies.TryGetValue("EndDateString", out string endDateString))
            {
                endDate = DateTime.Parse(endDateString);
            }
            else
            {
                endDate = DateTime.Now;
            }


            if (Request.Cookies.TryGetValue("DriverString", out string driverString))
            {
                driver = Convert.ToInt32(driverString);
            }
            else
            {
                driver = 0;
            }
            if (Request.Cookies.TryGetValue("OrganizationString", out string organizationString))
            {
                organization = Convert.ToInt32(organizationString);
            }
            else
            {
                organization = 0;
            }
            if (Request.Cookies.TryGetValue("LoadString", out string loadString))
            {
                load = Convert.ToInt32(loadString);
            }
            else
            {
                load = 0;
            }

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
                endTariff = 1000;
            }
            if (!string.IsNullOrEmpty(registrationNumber))
            {
                cargoTransportations = cargoTransportations.Where(u => u.Car.RegistrationNumber.ToUpperInvariant().Contains(registrationNumber.ToUpperInvariant())).ToList();
            }
            if (car != null && car != 0)
            {
                cargoTransportations = cargoTransportations.Where(t => t.CarId == car).ToList();
            }
            if (driver != null && driver != 0)
            {
                cargoTransportations = cargoTransportations.Where(t => t.DriverId == driver).ToList();
            }
            if (organization != null && organization != 0)
            {
                cargoTransportations = cargoTransportations.Where(t => t.OrganizationId == organization).ToList();
            }
            if (load != null && load != 0)
            {
                cargoTransportations = cargoTransportations.Where(t => t.LoadId == load).ToList();
            }

            if (startTariff != null && startTariff >= 0)
            {
                cargoTransportations = cargoTransportations.Where(t => t.TransportationTariff.TariffPerTKm >= startTariff).Where(t => t.TransportationTariff.TariffPerM3Km >= startTariff).ToList();
            }
            if (endTariff >= 0 && startTariff <= endTariff && endTariff != null)
            {
                cargoTransportations = cargoTransportations.Where(t => t.TransportationTariff.TariffPerTKm <= endTariff).Where(t => t.TransportationTariff.TariffPerM3Km <= endTariff).ToList();
            }

            if (startDistance != null && startDistance >= 0)
            {
                cargoTransportations = cargoTransportations.Where(t => t.Distance.Distance1 >= startDistance).ToList();
            }
            if (endDistance >= 0 && startDistance <= endDistance && endDistance != null)
            {
                cargoTransportations = cargoTransportations.Where(t => t.Distance.Distance1 <= endDistance).ToList();
            }

            if (startDate != null && startDate >= default(DateTime))
            {
                cargoTransportations = cargoTransportations.Where(t => t.Date >= startDate).ToList();
            }
            if (endDate >= default(DateTime) && startDate <= endDate && endDate != null)
            {
                cargoTransportations = cargoTransportations.Where(t => t.Date <= endDate).ToList();
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
            FilterCargoTransportationsViewModel filterCargoViewModel = new FilterCargoTransportationsViewModel( drivers, loads, organizations, 
                 registrationNumber, startDistance,endDistance,driver,load,organization,startTariff,endTariff,startDate,endDate);
            var viewModel = new CargoTransportationsViewModel(items, pageViewModel, filterCargoViewModel);

            return View(viewModel);
        }
        [Authorize]
        //(Roles = "admin")
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
        [Authorize]
        //(Roles = "admin")
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
        [Authorize]
        public async Task<IActionResult> UpdateIndex(string registrationNumber = "", int car = 0, int driver = 0, int load = 0, int organization = 0, int endTariff = 1000, int startTariff = 0, int startDistance = 0,
            int endDistance = 10000, DateTime startDate = default(DateTime), DateTime endDate = default(DateTime), int page = 1)
        {

            if (endDate == default(DateTime))
            {
                endDate = new DateTime(2030, 1, 1);
            }
            Response.Cookies.Delete("RegistrationNumber");
            Response.Cookies.Append("RegistrationNumber", registrationNumber);

            Response.Cookies.Delete("CarNumber");
            Response.Cookies.Append("CarNumber", car.ToString());

            Response.Cookies.Delete("DriverString");
            Response.Cookies.Append("DriverString", driver.ToString());

            Response.Cookies.Delete("OrganizationString");
            Response.Cookies.Append("OrganizationString", organization.ToString());

            Response.Cookies.Delete("LoadString");
            Response.Cookies.Append("LoadString", load.ToString());

            Response.Cookies.Delete("StartTariffString");
            Response.Cookies.Append("StartTariffString", startTariff.ToString());
            Response.Cookies.Delete("EndTariffString");
            Response.Cookies.Append("EndTariffString", endTariff.ToString());

            Response.Cookies.Delete("StartDistanceString");
            Response.Cookies.Append("StartDistanceString", startDistance.ToString());
            Response.Cookies.Delete("EndDistanceString");
            Response.Cookies.Append("EndDistanceString", endDistance.ToString());

            Response.Cookies.Delete("StartDateString");
            Response.Cookies.Append("StartDateString", startDate.ToString());
            Response.Cookies.Delete("EndDateString");
            Response.Cookies.Append("EndDateString", endDate.ToString());

            Response.Cookies.Delete("CargoPage");
            Response.Cookies.Append("CargoPage", page.ToString());


            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]

        //(Roles = "admin")
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
