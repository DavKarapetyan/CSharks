using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<User> _userManager;
        public MessageController(IMessageService messageService, UserManager<User> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var data = _messageService.GetAll();
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            MessageVM model = id.HasValue ? _messageService.GetForEdit(id.Value) : new MessageVM() { Id = 0};
            ViewBag.Users = _userManager.Users.ToList();
            return PartialView("_AddEdit", model);
        }
        [HttpPost]
        public IActionResult AddEdit(MessageVM model) 
        {
            if (model.Id == 0)
            {
                _messageService.Add(model);
            }
            else 
            {
                _messageService.Update(model);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete() 
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        { 
            _messageService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
