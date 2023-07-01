using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuizScoreController : Controller
    {
        private readonly IQuizScoreService _quizScoreService;
        private readonly UserManager<User> _userManager;
        private readonly IQuestionService _questionService;
        private readonly IQuestionAnswerService _questionAnswerService;
        private readonly IQuizTypeService _quizTypeService;
        public QuizScoreController(IQuizScoreService quizScoreService, UserManager<User> userManager, IQuestionService questionService, IQuestionAnswerService questionAnswerService, IQuizTypeService quizTypeService)
        {
            _quizScoreService = quizScoreService;
            _userManager = userManager;
            _questionService = questionService;
            _questionAnswerService = questionAnswerService;
            _quizTypeService = quizTypeService;
        }
        public IActionResult Index()
        { 
            var data = _quizScoreService.GetQuizScores();
            return View(data);
        }

        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            QuizScoreAddEditVM model = id.HasValue ? _quizScoreService.GetQuizScoreForEdit(id.Value) : new QuizScoreAddEditVM() { Id = 0};
            ViewBag.Users = _userManager.Users.ToList();
            ViewBag.Questions = _questionService.GetAllQuestion();
            ViewBag.QuestionAnswers = _questionAnswerService.GetQuestionAnswers();
            ViewBag.QuizTypes = _quizTypeService.GetQuizTypes();
            return PartialView("_AddEdit", model);
        }

        [HttpPost]
        public IActionResult AddEdit(QuizScoreAddEditVM model)
        {
            if (model.Id == 0)
            {
                _quizScoreService.Add(model);
            }
            else
            {
                _quizScoreService.Update(model);
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
            _quizScoreService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
