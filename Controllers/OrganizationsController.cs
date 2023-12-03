using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        // GET: /Settlements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Settlements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateOrganizationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the model entity
                var organization = new Organization
                {
                    OrganizationName = model.OrganizationName
                };

                // Save the tariff to the database
                _db.Organizations.Add(organization);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Organization organization = _db.Organizations.FirstOrDefault(t => t.OrganizationId == id);

            if (organization == null)
            {
                return NotFound();
            }


            EditOrganizationsViewModel model = new EditOrganizationsViewModel(organization.OrganizationName);

            model.Id = organization.OrganizationId;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditOrganizationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Organization organization = _db.Organizations.FirstOrDefault(t => t.OrganizationId == model.Id);

                if (organization != null)
                {

                    organization.OrganizationName = model.OrganizationName;



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

            List<Organization> organizations = _db.Organizations.ToList();


            int page;
            string organizationName;
            if (!Request.Cookies.TryGetValue("OrganizationName", out organizationName))
            {
                organizationName = "";
            }





            if (!string.IsNullOrEmpty(organizationName))
            {
                organizations = organizations.Where(u => u.OrganizationName.ToUpperInvariant().Contains(organizationName.ToUpperInvariant())).ToList();
            }




            if (Request.Cookies.TryGetValue("OrganizationPage", out string pageString))
            {
                page = Convert.ToInt32(pageString);
            }
            else
            {
                page = 1;
            }
            var items = organizations.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(organizations.Count, page, pageSize);
            FilterOrganizationsViewModel filterOrganizationsViewModel = new FilterOrganizationsViewModel(organizationName);
            var viewModel = new OrganizationsViewModel(items, pageViewModel, filterOrganizationsViewModel);

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIndex(string organizationName = "", int page = 1)
        {


            Response.Cookies.Delete("OrganizationName");
            Response.Cookies.Append("OrganizationName", organizationName);




            Response.Cookies.Delete("OrganizationPage");
            Response.Cookies.Append("OrganizationPage", page.ToString());


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Organization organization = _db.Organizations.FirstOrDefault(t => t.OrganizationId == id);

            if (organization != null)
            {
                _db.Organizations.Remove(organization);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
