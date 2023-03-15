using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsService _newsService;
        private readonly ICommentService _commentService;
        public NewsController(INewsService newsService, ICommentService commentService)
        {
            _newsService = newsService;
            _commentService = commentService;
        }

        public IActionResult Index(string? text,NewsType? newsType)
        {
            var AllNews = _newsService.GetAllNews(CurrentCulture, text, newsType);
            return View(AllNews);
        }
        public IActionResult NewsSingle(int id)
        {
            var data = _newsService.GetNewsById(id, CurrentCulture);
            return View(data);
        }
        [HttpGet]
        public IActionResult AddComment(int newsId)
        {
            return PartialView("_AddComment");
        }
        [HttpPost]
        public IActionResult AddComment(CommentAddEditVM model)
        {
            _commentService.Add(model);
            return Redirect($"/News/NewsSingle/{model.NewsId}");
        }
    }
}
