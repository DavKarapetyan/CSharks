using CSharks.BLL.Services.Interfaces;
using CSharks.DAL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Index(string? text,NewsType? newsType)
        {
            var AllNews = _newsService.GetAllNews(CultureType.en, text, newsType);
            return View(AllNews);
        }
        public IActionResult NewsSingle(int id)
        {
            var data = _newsService.GetNewsById(id, CultureType.en);
            return View(data);
        }
    }
}
