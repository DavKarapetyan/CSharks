using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuizTypeController : Controller
    {
        private readonly IQuizTypeService _quizTypeService;
        public QuizTypeController(IQuizTypeService quizTypeService)
        {
            _quizTypeService = quizTypeService;
        }

        public IActionResult Index()
        {
            var data = _quizTypeService.GetQuizTypes();
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id,CultureType culture) {
            QuizTypeVM model = id.HasValue ? _quizTypeService.GetQuizTypeById(id.Value) : new QuizTypeVM() { Id = 0 };
            model.Culture = culture;
            return PartialView("_AddEdit",model);
        }
        [HttpPost]
        public IActionResult AddEdit(QuizTypeVM model) {
            if (model.Id == 0)
            {
                _quizTypeService.Add(model);
            }
            else {
                _quizTypeService.Update(model, model.Culture);
            }
            return RedirectToAction("Index");
        }
    }
}
