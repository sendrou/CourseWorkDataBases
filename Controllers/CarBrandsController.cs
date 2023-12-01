using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class CarBrandsController : Controller
    {

        private readonly CargoContext _db;

        public CarBrandsController(CargoContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int numberRows = 100;

            List<CarBrand> carBrands = _db.CarBrands.Take(numberRows).ToList();


            return PartialView(carBrands);
        }
    }
}
