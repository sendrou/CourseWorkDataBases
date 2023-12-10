using Cargo.Models;
using Cargo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cargo.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly CargoContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager, CargoContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIndex(string name = "", int page = 1)
        {
            Response.Cookies.Delete("userName");
            Response.Cookies.Append("userName", name);

            Response.Cookies.Delete("userPage");
            Response.Cookies.Append("userPage", page.ToString());

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            string name;
            int page;

            if(!Request.Cookies.TryGetValue("userName", out name))
            {
                name = "";
            }
            if(Request.Cookies.TryGetValue("userPage", out string pageString))
            {
                page = Convert.ToInt32(pageString);
            }
            else
            {
                page = 1;
            }

            int pageSize = 10;

            var users = _userManager.Users.ToList();

            if(!string.IsNullOrEmpty(name))
            {
                users = users.Where(u => u.UserName.ToUpperInvariant().Contains(name.ToUpperInvariant())).ToList();
            }

            int count = users.Count;
            var items = users.Skip((page-1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            FilterUserViewModel filterUserViewModel = new FilterUserViewModel(name);
            UsersViewModel viewModel = new UsersViewModel(items, pageViewModel, filterUserViewModel);

            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Login
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role); 
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }


            List<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();

            return View(model);
        }


        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Login = user.UserName,

            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null) 
                {
                    user.Email = model.Email;
                    user.UserName = model.Login;


                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach(var error  in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

      
    }
}
