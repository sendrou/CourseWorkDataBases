using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class CarsController : Controller
    {
        private readonly CargoContext _db;

        public SettlementsController(CargoContext db)
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
        public IActionResult Create(CreateSettlementsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the model entity
                var settlement = new Settlement
                {
                    SettlementName = model.SettlementName
                };

                // Save the tariff to the database
                _db.Settlements.Add(settlement);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Settlement settlement = _db.Settlements.FirstOrDefault(t => t.SettlementId == id);

            if (settlement == null)
            {
                return NotFound();
            }


            EditSettlementsViewModel model = new EditSettlementsViewModel(settlement.SettlementName);

            model.Id = settlement.SettlementId;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSettlementsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Settlement settlement = _db.Settlements.FirstOrDefault(t => t.SettlementId == model.Id);

                if (settlement != null)
                {

                    settlement.SettlementName = model.SettlementName;



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

            List<Settlement> settlements = _db.Settlements.ToList();


            int page;
            string settlementName;
            if (!Request.Cookies.TryGetValue("SettlementName", out settlementName))
            {
                settlementName = "";
            }





            if (!string.IsNullOrEmpty(settlementName))
            {
                settlements = settlements.Where(u => u.SettlementName.ToUpperInvariant().Contains(settlementName.ToUpperInvariant())).ToList();
            }




            if (Request.Cookies.TryGetValue("SettlementPage", out string pageString))
            {
                page = Convert.ToInt32(pageString);
            }
            else
            {
                page = 1;
            }
            var items = settlements.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(settlements.Count, page, pageSize);
            FilterSettlementsViewModel filterSettlementsViewModel = new FilterSettlementsViewModel(settlementName);
            var viewModel = new SettlementsViewModel(items, pageViewModel, filterSettlementsViewModel);

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIndex(string settlementName = "", int page = 1)
        {


            Response.Cookies.Delete("SettlementName");
            Response.Cookies.Append("SettlementName", settlementName);




            Response.Cookies.Delete("SettlementPage");
            Response.Cookies.Append("SettlementPage", page.ToString());


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Settlement settlement = _db.Settlements.FirstOrDefault(t => t.SettlementId == id);

            if (settlement != null)
            {
                _db.Settlements.Remove(settlement);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
