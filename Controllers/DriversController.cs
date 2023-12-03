using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class DriversController : Controller
    {

        private readonly CargoContext _db;

        public DriversController(CargoContext db)
        {
            _db = db;
        }

        // GET: /Settlements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Settlements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateDriversViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the model entity
                var driver = new Driver
                {
                    FullName = model.DriverName,
                    PassportDetails = model.PassportDetails,
                };

                // Save the tariff to the database
                _db.Drivers.Add(driver);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Driver driver = _db.Drivers.FirstOrDefault(t => t.DriverId == id);

            if (driver == null)
            {
                return NotFound();
            }


            EditDriversViewModel model = new EditDriversViewModel(driver.FullName,driver.PassportDetails);

            model.Id = driver.DriverId;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDriversViewModel model)
        {
            if (ModelState.IsValid)
            {
                Driver driver = _db.Drivers.FirstOrDefault(t => t.DriverId == model.Id);

                if (driver != null)
                {

                    driver.FullName = model.DriverName;
                    driver.PassportDetails = model.PassportDetails;



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

            List<Driver> drivers = _db.Drivers.ToList();


            int page;
            string driverName, passportDetails;
            if (!Request.Cookies.TryGetValue("DriverName", out driverName))
            {
                driverName = "";
            }
            if (!Request.Cookies.TryGetValue("PassportDetails", out passportDetails))
            {
                passportDetails = "";
            }




            if (!string.IsNullOrEmpty(driverName))
            {
                drivers = drivers.Where(u => u.FullName.ToUpperInvariant().Contains(driverName.ToUpperInvariant())).ToList();
            }
            if (!string.IsNullOrEmpty(passportDetails))
            {
                drivers = drivers.Where(u => u.PassportDetails.ToUpperInvariant().Contains(passportDetails.ToUpperInvariant())).ToList();
            }



            if (Request.Cookies.TryGetValue("DriverPage", out string pageString))
            {
                page = Convert.ToInt32(pageString);
            }
            else
            {
                page = 1;
            }
            var items = drivers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(drivers.Count, page, pageSize);
            FilterDriversViewModel filterDriversViewModel = new FilterDriversViewModel(driverName,passportDetails);
            var viewModel = new DriversViewModel(items, pageViewModel, filterDriversViewModel);

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIndex(string driverName = "",string passportDetails="", int page = 1)
        {


            Response.Cookies.Delete("DriverName");
            Response.Cookies.Append("DriverName", driverName);

            Response.Cookies.Delete("PassportDetails");
            Response.Cookies.Append("PassportDetails", passportDetails);


            Response.Cookies.Delete("DriverPage");
            Response.Cookies.Append("DriverPage", page.ToString());


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Driver driver = _db.Drivers.FirstOrDefault(t => t.DriverId == id);

            if (driver != null)
            {
                _db.Drivers.Remove(driver);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
