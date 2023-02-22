using CSharks.BLL.Services.Interfaces;
using CSharks.DAL.Repositories.Interfaces;
using jdk.nashorn.@internal.ir;
using Microsoft.AspNetCore.Mvc;

namespace CSharks.Aplication.Controllers
{
    public class QuizController : BaseController
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
        public IActionResult GetQuestions(int quizTypeId,string QuizType) 
        {
            var data = _questionService.GetQuestionByQuizTypeId(quizTypeId);
            ViewBag.QuizType = QuizType;
            return View(data);
        }
        public IActionResult Question (int prev, int quizType)
        {
            //get next question by prev and pass model to view
            var data = _questionService.GetQuestionByQuizTypeId(quizType);
            var item = data.Skip(prev).FirstOrDefault();
            return PartialView("_Question",item);
        }
        public bool CheckAnswer(int questionAnswerId)
        {
            bool isRight = _questionAnswerService.IsCorrect(questionAnswerId);
            return isRight;
        }
    }
}