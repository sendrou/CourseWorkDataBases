using Cargo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class OrganizationsController : Controller
    {
        private readonly CargoContext _db;

        public OrganizationsController(CargoContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            int numberRows = 100;
            List<Organization> organizations = _db.Organizations.Take(numberRows).ToList();
           
            return PartialView(organizations);
        }
    }
}
