using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class CarBrandsController : Controller
    {

        private readonly CargoContext _db;
        private readonly IMemoryCache _cache;
        public CarBrandsController(CargoContext db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
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
        public IActionResult Create(CreateCarBrandsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the model entity
                var carBrand = new CarBrand
                {
                    BrandName = model.CarBrand,
                };

                // Save the tariff to the database
                _db.CarBrands.Add(carBrand);
                _db.SaveChanges();
                _cache.Remove("carBrands");

                return RedirectToAction("Index");
            }

            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            CarBrand carBrand = _db.CarBrands.FirstOrDefault(t => t.CarBrandId == id);

            if (carBrand == null)
            {
                return NotFound();
            }


            EditCarBrandsViewModel model = new EditCarBrandsViewModel(carBrand.BrandName);

            model.Id = carBrand.CarBrandId;


            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(EditCarBrandsViewModel model)
        {
            if (ModelState.IsValid)
            {
                CarBrand carBrand = _db.CarBrands.FirstOrDefault(t => t.CarBrandId == model.Id);

                if (carBrand != null)
                {

                    carBrand.BrandName = model.CarBrand;



                    _db.SaveChanges();
                    _cache.Remove("carBrands");


                    return RedirectToAction("Index");

                }
            }
            List<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();



            return View(model);
        }
        [Authorize]
        public IActionResult Index()
        {
            int pageSize = 10;

            //List<CarBrand> carBrands = _db.CarBrands.ToList();

            _cache.TryGetValue("carBrands", out List<CarBrand> carBrands);
            if (carBrands == null)
            {
                carBrands = _db.CarBrands.ToList();

                if (carBrands != null)
                {
                    _cache.Set("carBrands", carBrands, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }

                Console.WriteLine("Данные были взяти из памяти.");
            }
            else
            {
                Console.WriteLine("Данные были взяти их кэша.");
            }
            int page;
            string brandName;
            if (!Request.Cookies.TryGetValue("BrandName", out brandName))
            {
                brandName = "";
            }





            if (!string.IsNullOrEmpty(brandName))
            {
                carBrands = carBrands.Where(u => u.BrandName.ToUpperInvariant().Contains(brandName.ToUpperInvariant())).ToList();
            }




            if (Request.Cookies.TryGetValue("CarBrandPage", out string pageString))
            {
                page = Convert.ToInt32(pageString);
            }
            else
            {
                page = 1;
            }
            var items = carBrands.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(carBrands.Count, page, pageSize);
            FilterCarBrandsViewModel filterCarBrandsViewModel = new FilterCarBrandsViewModel(brandName);
            var viewModel = new CarBrandsViewModel(items, pageViewModel, filterCarBrandsViewModel);

            return View(viewModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateIndex(string brandName = "", int page = 1)
        {


            Response.Cookies.Delete("BrandName");
            Response.Cookies.Append("BrandName", brandName);




            Response.Cookies.Delete("CarBrandPage");
            Response.Cookies.Append("CarBrandPage", page.ToString());


            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            CarBrand carBrand = _db.CarBrands.FirstOrDefault(t => t.CarBrandId == id);

            if (carBrand != null)
            {
                _db.CarBrands.Remove(carBrand);
                _db.SaveChanges();
                _cache.Remove("carBrands");
            }

            return RedirectToAction("Index");
        }
    }
}
