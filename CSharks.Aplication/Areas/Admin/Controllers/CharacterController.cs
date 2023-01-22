using CSharks.BLL.Services;
using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CharacterController : Controller
    {
        private readonly ICharacterService _characterService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CharacterController(ICharacterService characterService, IWebHostEnvironment webHostEnvironment)
        {
            _characterService = characterService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var data = _characterService.GetAllCharacters(CultureType.en);
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id, CultureType culture)
        {
            CharacterVM model = id.HasValue ? _characterService.GetCharacterForEdit(id.Value, CultureType.en) : new CharacterVM() { Id = 0 };
            model.CultureType = culture;
            return PartialView("_AddEdit", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(CharacterVM model, IFormFile fileName)
        {
            if (fileName != null)
            {
                string path = "/Files/Characters/" + fileName.FileName;
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create)) { await fileName.CopyToAsync(fileStream); }
                model.ImageFile = path;
                if (model.Id == 0)
                {
                    _characterService.Add(model);
                }
                else
                {
                    _characterService.Update(model, model.CultureType);
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
            _characterService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
