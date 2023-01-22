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
    public class QuestionAnswerController : Controller
    {
        private readonly IQuestionAnswerService _questionAnswerService;
        public QuestionAnswerController(IQuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
        }
        public IActionResult Index()
        {
            var data = _questionAnswerService.GetQuestionAnswers();
            return View(data);
        }
        [HttpGet]
        public IActionResult AddEdit(int? id, int? questionId,CultureType culture) {
            QuestionAnswerAddEditVM model = id.HasValue ? _questionAnswerService.GetForEdit(id.Value) : new QuestionAnswerAddEditVM() { Id = 0,QuestionId = questionId.Value};
            model.Culture = culture;
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
