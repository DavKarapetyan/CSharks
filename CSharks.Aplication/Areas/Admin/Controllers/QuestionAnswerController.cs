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
    [Authorize(Roles = "Admin")]

    public class QuestionAnswerController : Controller
    {
        private readonly IQuestionAnswerService _questionAnswerService;
        private readonly IQuestionService _questionService;
        public QuestionAnswerController(IQuestionAnswerService questionAnswerService,IQuestionService questionService)
        {
            _questionAnswerService = questionAnswerService;
            _questionService = questionService;
        }
        public IActionResult Index()
        {
            var data = _questionAnswerService.GetQuestionAnswers(CultureType.en);
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id, int? questionId,CultureType culture) {
            QuestionAnswerAddEditVM model = id.HasValue ? _questionAnswerService.GetForEdit(id.Value, culture) : new QuestionAnswerAddEditVM() { Id = 0,QuestionId = questionId.HasValue ? questionId.Value : 0};
            model.Culture = culture;
            ViewBag.Questions = _questionService.GetAllQuestion(CultureType.en);
            return PartialView("_AddEdit",model);
        }
        [HttpPost]
        public IActionResult AddEdit(QuestionAnswerAddEditVM model) {
            if(model.Id == 0)
            {
                _questionAnswerService.Add(model);
            }
            else
            {
                _questionAnswerService.Update(model,model.Culture);
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
            _questionAnswerService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
