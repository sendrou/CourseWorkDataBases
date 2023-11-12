using Lab02;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab04.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class OrganizationsController : Controller
    {
        private readonly Lab1Context _db;

        public OrganizationsController(Lab1Context db)
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
