using CSharks.BLL.Services;
using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IQuizTypeService _quizTypeService;
        public QuestionController(IQuestionService questionService,IQuizTypeService quizTypeService)
        {
            _questionService = questionService;
            _quizTypeService = quizTypeService;
        }

        public IActionResult Index()
        {
            var data = _questionService.GetAllQuestion(CultureType.en);
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id,CultureType culture) { 
            QuestionAddEditVM model = id.HasValue ? _questionService.GetQuestionForEdit(id.Value, culture) : new QuestionAddEditVM() {Id = 0};
            ViewBag.QuestionTypes = _quizTypeService.GetQuizTypes(CultureType.en);
            model.Culture = culture;
            return PartialView("_AddEdit",model);
        }
        [HttpPost]
        public IActionResult AddEdit(QuestionAddEditVM model) {
            if (model.Id == 0)
            {
                _questionService.Add(model);
            }
            else {
                _questionService.Update(model,model.Culture);
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
            _questionService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
