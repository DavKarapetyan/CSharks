using CSharks.BLL.Services;
using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IQuizTypeService _quizTypeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public QuestionController(IQuestionService questionService,IQuizTypeService quizTypeService, IWebHostEnvironment webHostEnvironment)
        {
            _questionService = questionService;
            _quizTypeService = quizTypeService;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> AddEdit(QuestionAddEditVM model, IFormFile fileName) {
            if (fileName != null)
            {
                string path = "/Files/" + fileName.FileName;
                using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create)) { await fileName.CopyToAsync(fileStream); }
                model.QuestionImage = path;

            }
            if (model.Id == 0)
            {
                _questionService.Add(model);
            }
            else
            {
                _questionService.Update(model, model.Culture);
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
