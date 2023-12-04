using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class DistancesController : Controller
    {
        private readonly CargoContext _db;

        public DistancesController(CargoContext db)
        {
            _db = db;
        }

        // GET: /Settlements/Create
        public IActionResult Create()
        {
            List<Settlement> settlements = _db.Settlements.ToList();
            CreateDistancesViewModel viewModel = new CreateDistancesViewModel(settlements);

            return View(viewModel);
        }

        // POST: /Settlements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateDistancesViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the model entity
                var distance = new Distance
                {
                    DeparturesSettlementId = model.DeparturesSettlementId,
                    ArrivalSettlementId = model.ArrivalSettlementId,
                    Distance1 = model.Distance,
                };

                // Save the tariff to the database
                _db.Distances.Add(distance);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Distance distance = _db.Distances.FirstOrDefault(t => t.DistanceId == id);

            if (distance == null)
            {
                return NotFound();
            }
            List<Settlement> settlements = _db.Settlements.ToList();

            EditDistancesViewModel model = new EditDistancesViewModel(settlements,distance.ArrivalSettlementId,distance.DeparturesSettlementId,distance.Distance1);

            model.Id = distance.DistanceId;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditDistancesViewModel model)
        {
            if (ModelState.IsValid)
            {
                Distance distance = _db.Distances.FirstOrDefault(t => t.DistanceId == model.Id);

                if (distance != null)
                {

                    distance.ArrivalSettlementId = model.SelectedArrivalSettlementId;
                    distance.DeparturesSettlementId = model.SelectedDeparturesSettlementId;
                    distance.Distance1 = model.SelectedDistance;


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

            List<Distance> distances = _db.Distances.ToList();

            List<Settlement> settlements = _db.Settlements.ToList();
          

            int page, startDistance, endDistance;
            string departuresSettlementName,arrivalSettlementName;

         

            if (!Request.Cookies.TryGetValue("DeparturesSettlementName", out departuresSettlementName))
            {
                departuresSettlementName = "";
            }
            if (!Request.Cookies.TryGetValue("ArrivalSettlementName", out arrivalSettlementName))
            {
                arrivalSettlementName = "";
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


            if (startDistance != null && startDistance >= 0)
            {
                distances = distances.Where(t => t.Distance1 >= startDistance).ToList();
            }
            if (endDistance >= 0 && startDistance <= endDistance && endDistance != null)
            {
                distances = distances.Where(t => t.Distance1 <= endDistance).ToList();
            }

            if (!string.IsNullOrEmpty(departuresSettlementName))
            {
                distances = distances.Where(u => u.DeparturesSettlement.SettlementName.ToUpperInvariant().Contains(departuresSettlementName.ToUpperInvariant())).ToList();
            }
            if (!string.IsNullOrEmpty(arrivalSettlementName))
            {
                distances = distances.Where(u => u.ArrivalSettlement.SettlementName.ToUpperInvariant().Contains(arrivalSettlementName.ToUpperInvariant())).ToList();
            }



            if (Request.Cookies.TryGetValue("DistancePage", out string pageString))
            {
                page = Convert.ToInt32(pageString);
            }
            else
            {
                page = 1;
            }
            int settlement = 0;
            var items = distances.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(distances.Count, page, pageSize);
            FilterDistancesViewModel filterDistancesViewModel = new FilterDistancesViewModel(settlements,settlement,startDistance, endDistance,departuresSettlementName,arrivalSettlementName);
            var viewModel = new DistancesViewModel(items, pageViewModel, filterDistancesViewModel);

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIndex(string departuresSettlementName = "", string arrivalSettlementName = "", int endDistance = 10000, int startDistance = 0,int page = 1)
        {


            Response.Cookies.Delete("DeparturesSettlementName");
            Response.Cookies.Append("DeparturesSettlementName", departuresSettlementName);

            Response.Cookies.Delete("ArrivalSettlementName");
            Response.Cookies.Append("ArrivalSettlementName", arrivalSettlementName);

            Response.Cookies.Delete("StartDistanceString");
            Response.Cookies.Append("StartDistanceString", startDistance.ToString());
            Response.Cookies.Delete("EndDistanceString");
            Response.Cookies.Append("EndDistanceString", endDistance.ToString());

       

            Response.Cookies.Delete("DistancePage");
            Response.Cookies.Append("DistancePage", page.ToString());


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Distance distance = _db.Distances.FirstOrDefault(t => t.DistanceId == id);

            if (distance != null)
            {
                _db.Distances.Remove(distance);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
