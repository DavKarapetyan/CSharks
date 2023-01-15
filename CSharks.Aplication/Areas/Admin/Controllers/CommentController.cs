using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly INewsService _newsService;
        public CommentController(ICommentService commentService,INewsService newsService)
        {
            _commentService = commentService;
            _newsService = newsService;
        }
        public IActionResult Index()
        {
            var data = _commentService.GetComments();
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id) {
            CommentAddEditVM model = id.HasValue ? _commentService.GetCommentForEdit(id.Value) : new CommentAddEditVM() { Id = 0};
            ViewBag.News = _newsService.GetAllNews(CultureType.en); 
            return PartialView("_AddEdit",model);
        }
        [HttpPost]
        public IActionResult AddEdit(CommentAddEditVM model) {
            if (model.Id == 0)
            {
                _commentService.Add(model);
            }
            else { 
                _commentService.Update(model);
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
            _commentService.Delete(id);
            return RedirectToAction("Index");   
        }
    }
}
