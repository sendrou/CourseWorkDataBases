using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class CarsController : Controller
    {
        private readonly CargoContext _db;

        public CarsController(CargoContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Settlements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(CreateCarsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the model entity
                var car = new Car
                {
                    BodyVolume = model.BodyVolume,
                    LiftingCapacity = model.LiftingCapacity,
                    RegistrationNumber = model.RegistrationNumber,
                    CarBrandId = model.CarBrandId,
                };

                // Save the tariff to the database
                _db.Cars.Add(car);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Car car = _db.Cars.FirstOrDefault(t => t.CarId == id);

            if (car == null)
            {
                return NotFound();
            }
            List<CarBrand> carBrands = _db.CarBrands.ToList();

            EditCarsViewModel model = new EditCarsViewModel(carBrands, car.CarBrandId,car.LiftingCapacity,car.BodyVolume,car.RegistrationNumber);

            model.Id = car.CarId;


            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(EditCarsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Car car = _db.Cars.FirstOrDefault(t => t.CarId == model.Id);

                if (car != null)
                {

                    car.CarBrandId = model.SelectedCarBrandId;
                    car.LiftingCapacity = model.LiftingCapacity; 
                    car.BodyVolume = model.BodyVolume;
                    car.RegistrationNumber = model.RegistrationNumber;


                    _db.SaveChanges();


                    return RedirectToAction("Index");

                }
            }
            List<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();

            model.CarBrands = new SelectList(_db.CarBrands.ToList(), "CarBrandId", "BrandName");

            return View(model);
        }
        [Authorize]

        public IActionResult Index()
        {
            int pageSize = 10;

            List<Car> cars = _db.Cars.ToList();
            List<CargoTransportation> cargo = _db.CargoTransportations.Include("Load").Include("Distance").Include("TransportationTariff").ToList();
            List<CarBrand> carBrands = _db.CarBrands.ToList();
            carBrands.Insert(0, new CarBrand { BrandName = "Все", CarBrandId = 0 });

            int page,startLiftingCapacity, endLiftingCapacity, startBodyVolume, endBodyVolume, carBrand;
            string registrationNumber;
            if (!Request.Cookies.TryGetValue("RegistrationNumber", out registrationNumber))
            {
                registrationNumber = "";
            }

            if (Request.Cookies.TryGetValue("StartBodyVolume", out string startBodyVolumeString))
            {
                startBodyVolume = Convert.ToInt32(startBodyVolumeString);
            }
            else
            {
                startBodyVolume = 0;
            }

            if (Request.Cookies.TryGetValue("EndBodyVolume", out string endBodyVolumeString))
            {
                endBodyVolume = Convert.ToInt32(endBodyVolumeString);
            }
            else
            {
                endBodyVolume = 10000;
            }

            if (Request.Cookies.TryGetValue("StartLiftingCapacity", out string startLiftingCapacityString))
            {
                startLiftingCapacity = Convert.ToInt32(startLiftingCapacityString);
            }
            else
            {
                startLiftingCapacity = 0;
            }

            if (Request.Cookies.TryGetValue("EndLiftingCapacity", out string endLiftingCapacityString))
            {
                endLiftingCapacity = Convert.ToInt32(endLiftingCapacityString);
            }
            else
            {
                endLiftingCapacity = 10000;
            }

            if (Request.Cookies.TryGetValue("CarBrandString", out string carBrandString))
            {
                carBrand = Convert.ToInt32(carBrandString);
            }
            else
            {
                carBrand = 0;
            }

            if (startBodyVolume != null && startBodyVolume >= 0)
            {
                cars = cars.Where(t => t.BodyVolume >= startBodyVolume).ToList();
            }
            if (endBodyVolume >= 0 && startBodyVolume <= endBodyVolume && endBodyVolume != null)
            {
                cars = cars.Where(t => t.BodyVolume <= endBodyVolume).ToList();
            }
            if (startLiftingCapacity != null && startLiftingCapacity >= 0)
            {
                cars = cars.Where(t => t.LiftingCapacity >= startLiftingCapacity).ToList();
            }
            if (endLiftingCapacity >= 0 && startLiftingCapacity <= endLiftingCapacity && endLiftingCapacity != null)
            {
                cars = cars.Where(t => t.LiftingCapacity <= endLiftingCapacity).ToList();
            }
            if (carBrand != null && carBrand != 0)
            {
                cars = cars.Where(t => t.CarBrandId == carBrand).ToList();
            }

            if (!string.IsNullOrEmpty(registrationNumber))
            {
                cars = cars.Where(u => u.RegistrationNumber.ToUpperInvariant().Contains(registrationNumber.ToUpperInvariant())).ToList();
            }




            if (Request.Cookies.TryGetValue("CarPage", out string pageString))
            {
                page = Convert.ToInt32(pageString);
            }
            else
            {
                page = 1;
            }
            DateTime startDate, endDate;
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

            var items = cars.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(cars.Count, page, pageSize);
            FilterCarsViewModel filterSettlementsViewModel = new FilterCarsViewModel(carBrands, carBrand,startBodyVolume,endBodyVolume,startLiftingCapacity,endLiftingCapacity ,registrationNumber,startDate,endDate);
            var viewModel = new CarsViewModel(items,cargo, pageViewModel, filterSettlementsViewModel);

            return View(viewModel);
        }
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> UpdateIndex(string registrationNumber = "",int startBodyVolume=0, int endBodyVolume =10000, int startLiftingCapacity =0, int endLiftingCapacity =10000,int carBrand=0, DateTime startDate = default(DateTime), DateTime endDate = default(DateTime), int page = 1)
        {
            if (endDate == default(DateTime))
            {
                endDate = new DateTime(2030, 1, 1);
            }

            Response.Cookies.Delete("RegistrationNumber");
            Response.Cookies.Append("RegistrationNumber", registrationNumber);

            Response.Cookies.Delete("CarBrandString");
            Response.Cookies.Append("CarBrandString", carBrand.ToString());

            Response.Cookies.Delete("StartBodyVolume");
            Response.Cookies.Append("StartBodyVolume", startBodyVolume.ToString());
            Response.Cookies.Delete("EndBodyVolume");
            Response.Cookies.Append("EndBodyVolume", endBodyVolume.ToString());

            Response.Cookies.Delete("StartLiftingCapacity");
            Response.Cookies.Append("StartLiftingCapacity", startLiftingCapacity.ToString());
            Response.Cookies.Delete("EndLiftingCapacity");
            Response.Cookies.Append("EndLiftingCapacity", endLiftingCapacity.ToString());

            Response.Cookies.Delete("StartDateString");
            Response.Cookies.Append("StartDateString", startDate.ToString());
            Response.Cookies.Delete("EndDateString");
            Response.Cookies.Append("EndDateString", endDate.ToString());

            Response.Cookies.Delete("CarPage");
            Response.Cookies.Append("CarPage", page.ToString());


            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            Car car = _db.Cars.FirstOrDefault(t => t.CarId == id);

            if (car != null)
            {
                _db.Cars.Remove(car);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
