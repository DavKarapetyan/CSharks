using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Migrations;
using CSharks.DAL.Repositories.Interfaces;
using jdk.nashorn.@internal.ir;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CSharks.Aplication.Controllers
{
    public class QuizController : BaseController
    {
        private readonly IQuizTypeService _quizTypeService;
        private readonly IQuestionService _questionService;
        private readonly IQuestionAnswerService _questionAnswerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IQuizScoreService _quizScoreService;

        public QuizController(IQuizTypeService quizTypeService, IQuestionService questionService, IQuestionAnswerService questionAnswerService, IHttpContextAccessor httpContextAccessor, IQuizScoreService quizScoreService)
        {
            _questionAnswerService = questionAnswerService;
            _quizTypeService = quizTypeService;
            _questionService = questionService;
            _httpContextAccessor = httpContextAccessor;
            _quizScoreService = quizScoreService;
        }
        public IActionResult Index()
        {
            var data = _quizTypeService.GetQuizTypes(CurrentCulture);
            ViewBag.GroupedQuizScores = _quizScoreService.GetGroupedQuizScore();
            return View(data);
        }
        public IActionResult GetQuestions(int quizTypeId, string QuizType)
        {
            var data = _questionService.GetQuestionByQuizTypeId(quizTypeId, CurrentCulture);
            //ViewBag.QuizType = QuizType;
            //ViewBag.QuizTypeDescription = _quizTypeService.GetQuizTypeById(quizTypeId, CurrentCulture).Description;
            return View(data);
        }
        public void AddQuizScore(int questionId, int questionAnswerId, int quizTypeId, int score)
        {
            QuizScoreAddEditVM model = new QuizScoreAddEditVM()
            {
                QuestionAnswerId = questionAnswerId,
                QuestionId = questionId,
                QuizTypeId = quizTypeId,
                Score = score,
                UserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value),
            };
            _quizScoreService.Add(model);
        }
        public IActionResult Question(int prev, int quizType)
        {
            //get next question by prev and pass model to view
            var data = _questionService.GetQuestionByQuizTypeId(quizType, CurrentCulture);
            var item = data.Skip(prev).FirstOrDefault();
            return PartialView("_Question", item);
        }
        public IActionResult Finished(int score, int quizTypeId)
        {
            ViewBag.QuizType = _quizTypeService.GetQuizTypeById(quizTypeId, CurrentCulture);
            return View(score);
        }
        public int GetQuestionCount(int quizTypeId)
        {
            return _questionService.GetQuestionByQuizTypeId(quizTypeId, CurrentCulture).Count;  
        }
        public bool CheckAnswer(int questionAnswerId)
        {
            bool isRight = _questionAnswerService.IsCorrect(questionAnswerId);
            return isRight;
        }
    }
}