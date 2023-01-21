using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public NewsController(INewsService newsService, IWebHostEnvironment webHostEnvironment)
        {
            _newsService = newsService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string? text, NewsType? newsType)
        {
            var data = _newsService.GetAllNews(CultureType.en,text,newsType);
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? newsId,CultureType culture)
        {
            NewsVM model = newsId.HasValue ? _newsService.GetNewsById(newsId.Value,CultureType.en) : new NewsVM() { Id = 0 };
            model.CultureType = culture;
            return PartialView("_AddEdit", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(NewsVM model, IFormFile fileName)
        {
            if (fileName != null)
            {
                string path = "/Files/" + fileName.FileName;
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create)) { await fileName.CopyToAsync(fileStream); }
                model.ImageFile = path;
                if (model.Id == 0)
                {
                    _newsService.Add(model);
                }
                else
                {
                    _newsService.Update(model,model.CultureType);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
