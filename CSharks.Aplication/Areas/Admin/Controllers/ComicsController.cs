using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ComicsController : Controller
    {
        private readonly IComicsService _comicsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ComicsController(IComicsService comicsService, IWebHostEnvironment webHostEnvironment)
        {
            _comicsService = comicsService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var data = _comicsService.GetAllComices(CultureType.en);
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id, CultureType culture) 
        {
            ComicsVM model = id.HasValue ? _comicsService.GetComicsForEdit(id.Value,culture) : new ComicsVM() { Id = 0};
            model.CultureType = culture;
            return PartialView("_AddEdit", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(ComicsVM model, IFormFile fileName)
        {
            if (fileName != null)
            { 
                string path = "/Files/Comics" + fileName.FileName;
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create)) { await fileName.CopyToAsync(fileStream); }
                model.ImageFile = path;
                if (model.Id == 0)
                {
                    _comicsService.Add(model);
                }
                else
                {
                    _comicsService.Update(model, model.CultureType);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete() 
        {
            return PartialView("_Delete");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _comicsService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
