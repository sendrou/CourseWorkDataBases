using Lab02;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class CarBrandsController : Controller
    {

        private readonly Lab1Context _db;

        public CarBrandsController(Lab1Context db)
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
