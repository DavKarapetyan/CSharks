using CSharks.BLL.Services.Interfaces;
using CSharks.DAL.Repositories.Interfaces;
using jdk.nashorn.@internal.ir;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Controllers
{
    public class QuizController : Controller
    {
        private readonly IQuizTypeService _quizTypeService;
        private readonly IQuestionService _questionService;
        private readonly IQuestionAnswerService _questionAnswerService;

        public QuizController(IQuizTypeService quizTypeService,IQuestionService questionService, IQuestionAnswerService questionAnswerService)
        {
            _questionAnswerService = questionAnswerService;
            _quizTypeService = quizTypeService;
            _questionService = questionService;
        }
        public IActionResult Index()
        {
            var data = _quizTypeService.GetQuizTypes();
            return View(data);
        }
        public IActionResult GetQuestions(int quizTypeId) 
        {
            var data = _questionService.GetQuestionByQuizTypeId(quizTypeId);
            return View(data);
        }
    }
}