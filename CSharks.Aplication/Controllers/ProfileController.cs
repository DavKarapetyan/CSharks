using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            { 
                return NotFound();
            }
            EditUserVM model = new EditUserVM() { Id = Convert.ToString(user.Id), Email = user.Email, DOB = user.DOB, Avatar = user.Avatar, NickName = user.NickName};
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(EditUserVM model)
        {
            if (ModelState.IsValid)
            { 
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.DOB = model.DOB;
                    user.Avatar = model.Avatar;
                    user.NickName = model.NickName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else 
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
    }
}
