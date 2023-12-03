using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    public class LoadsController : Controller
    {

        private readonly CargoContext _db;

        public LoadsController(CargoContext db)
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
        public IActionResult Create(CreateLoadsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the model entity
                var load = new Load
                {
                    LoadName = model.LoadName,
                    Volume = model.Volume,
                    Weight = model.Weight,
                };

                // Save the tariff to the database
                _db.Loads.Add(load);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Load load = _db.Loads.FirstOrDefault(t => t.LoadId == id);

            if (load == null)
            {
                return NotFound();
            }


            EditLoadsViewModel model = new EditLoadsViewModel(load.LoadName, load.Volume, load.Weight);

            model.Id = load.LoadId;


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditLoadsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Load load = _db.Loads.FirstOrDefault(t => t.LoadId == model.Id);

                if (load != null)
                {

                    load.LoadName = model.LoadName;
                    load.Volume = model.Volume;
                    load.Weight = model.Weight;


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

            List<Load> loads = _db.Loads.ToList();


            int page, startVolume, endVolume, startWeight, endWeight;
            string loadName;
            if (!Request.Cookies.TryGetValue("LoadName", out loadName))
            {
                loadName = "";
            }

            if (Request.Cookies.TryGetValue("StartVolume", out string startVolumeString))
            {
                startVolume = Convert.ToInt32(startVolumeString);
            }
            else
            {
                startVolume = 0;
            }

            if (Request.Cookies.TryGetValue("EndVolume", out string endVolumeString))
            {
                endVolume = Convert.ToInt32(endVolumeString);
            }
            else
            {
                endVolume = 10000;
            }
            if (Request.Cookies.TryGetValue("StartWeight", out string startWeightString))
            {
                startWeight = Convert.ToInt32(startWeightString);
            }
            else
            {
                startWeight = 0;
            }

            if (Request.Cookies.TryGetValue("EndWeight", out string endWeightString))
            {
                endWeight = Convert.ToInt32(endWeightString);
            }
            else
            {
                endWeight = 10000;
            }



            if (!string.IsNullOrEmpty(loadName))
            {
                loads = loads.Where(u => u.LoadName.ToUpperInvariant().Contains(loadName.ToUpperInvariant())).ToList();
            }
            if (startVolume != null && startVolume >= 0)
            {
                loads = loads.Where(t => t.Volume >= startVolume).ToList();
            }
            if (endVolume >= 0 && startVolume <= endVolume && endVolume != null)
            {
                loads = loads.Where(t => t.Volume <= endVolume).ToList();
            }
            if (startWeight != null && startWeight >= 0)
            {
                loads = loads.Where(t => t.Weight >= startWeight).ToList();
            }
            if (endWeight >= 0 && startWeight <= endWeight && endWeight != null)
            {
                loads = loads.Where(t => t.Weight <= endWeight).ToList();
            }


            if (Request.Cookies.TryGetValue("LoadPage", out string pageString))
            {
                page = Convert.ToInt32(pageString);
            }
            else
            {
                page = 1;
            }
            var items = loads.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(loads.Count, page, pageSize);
            FilterLoadsViewModel filterLoadsViewModel = new FilterLoadsViewModel(loadName,startVolume,endVolume,startWeight,endWeight);
            var viewModel = new LoadsViewModel(items, pageViewModel, filterLoadsViewModel);

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIndex(string loadName = "",int startVolume=0, int endVolume=10000, int startWeight=0, int endWeight=10000,  int page = 1)
        {


            Response.Cookies.Delete("LoadName");
            Response.Cookies.Append("LoadName", loadName);

            Response.Cookies.Delete("StartVolume");
            Response.Cookies.Append("StartVolume", startVolume.ToString());
            Response.Cookies.Delete("StartWeight");
            Response.Cookies.Append("StartWeight", startWeight.ToString());
            Response.Cookies.Delete("EndVolume");
            Response.Cookies.Append("EndVolume", endVolume.ToString());
            Response.Cookies.Delete("EndWeight");
            Response.Cookies.Append("EndWeight", endWeight.ToString());
            Response.Cookies.Delete("LoadPage");
            Response.Cookies.Append("LoadPage", page.ToString());


            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Load load = _db.Loads.FirstOrDefault(t => t.LoadId == id);

            if (load != null)
            {
                _db.Loads.Remove(load);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
