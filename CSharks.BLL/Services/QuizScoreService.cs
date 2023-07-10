using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services
{
    public class QuizScoreService : IQuizScoreService
    {
        private readonly IQuizScoreRepository _quizScoreRepository;
        private readonly IQuestionService _questionService;
        private readonly IQuestionAnswerService _questionAnswerService;
        private readonly IQuizTypeService _quizTypeService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public QuizScoreService(IQuizScoreRepository quizScoreRepository, IUnitOfWork uow, IQuestionService questionService, IQuestionAnswerService questionAnswer, IQuizTypeService quizTypeService, UserManager<User> userManager)
        {
            _quizScoreRepository = quizScoreRepository;
            _unitOfWork = uow;
            _questionService = questionService;
            _questionAnswerService = questionAnswer;
            _quizTypeService = quizTypeService;
            _userManager = userManager;
        }
        public void Add(QuizScoreAddEditVM model)
        {
            QuizScore quizScore = new QuizScore 
            { 
                Score = model.Score,
                UserId = model.UserId,
                QuestionAnswerId = model.QuestionAnswerId,
                QuestionId = model.QuestionId,
                QuizTypeId = model.QuizTypeId,
            };
            _quizScoreRepository.Add(quizScore);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _quizScoreRepository.Delete(id);
            _unitOfWork.Save();
        }

        public List<GroupedQuizScoreVM> GetGroupedQuizScore()
        {
            var quizScores = GetQuizScores();

            var list = quizScores.GroupBy(x => x.UserId);
            List<GroupedQuizScoreVM> groupedQuizScores = new List<GroupedQuizScoreVM>();
            foreach (var item in list)
            {
                GroupedQuizScoreVM groupedQuizScore = new GroupedQuizScoreVM()
                {
                    Id = item.Key,
                    Score = item.Sum(x => x.Score),
                    Avatar = _userManager.Users.Where(x => x.Id == item.FirstOrDefault().UserId).FirstOrDefault().Avatar.ToString(),
                    UserName = _userManager.Users.Where(x => x.Id == item.FirstOrDefault().UserId).FirstOrDefault().UserName,
                };
                groupedQuizScores.Add(groupedQuizScore);
            }
            groupedQuizScores = groupedQuizScores.Take(10).ToList();
            return groupedQuizScores;
        }

        public QuizScoreVM GetQuizScoreById(int id)
        {
            var quizScore = _quizScoreRepository.GetById(id);
            QuizScoreVM quizScoreVM = new QuizScoreVM
            {
                Id = id,
                QuestionAnswer = _questionAnswerService.GetQuestionAnswer(quizScore.QuestionAnswerId, DAL.Enums.CultureType.en),
                Score = quizScore.Score,
                UserId = quizScore.UserId,
                Question = _questionService.GetQuestionById(quizScore.QuestionId, DAL.Enums.CultureType.en),
                QuizType = _quizTypeService.GetQuizTypeById(quizScore.QuizTypeId, DAL.Enums.CultureType.en)
            };

            return quizScoreVM;
        }

        public QuizScoreAddEditVM GetQuizScoreForEdit(int id)
        {
            var quizScore = _quizScoreRepository.GetForEdit(id);
            QuizScoreAddEditVM model = new QuizScoreAddEditVM 
            {
                Id = id,
                QuestionAnswerId = quizScore.QuestionAnswerId,
                Score = quizScore.Score, 
                UserId = quizScore.UserId,
                QuestionId = quizScore.QuestionId,
                QuizTypeId = quizScore.QuizTypeId,
            };
            return model;
        }

        public List<QuizScoreVM> GetQuizScores()
        {
            var quizScores = _quizScoreRepository.GetAll().Select(n => new QuizScoreVM() 
            {
                Id = n.Id,
                Question = _questionService.GetQuestionById(n.QuestionId, DAL.Enums.CultureType.en),
                QuestionAnswer = _questionAnswerService.GetQuestionAnswer(n.QuestionAnswerId, DAL.Enums.CultureType.en),
                QuizType = _quizTypeService.GetQuizTypeById(n.QuizTypeId, DAL.Enums.CultureType.en),
                Score = n.Score,
                UserId = n.UserId,
            }).ToList();

            return quizScores;
        }

        public void Update(QuizScoreAddEditVM model)
        {
            var quizScore = new QuizScore() 
            {
                Id = model.Id,
                UserId = model.UserId,
                Score = model.Score,
                QuestionAnswerId = model.QuestionAnswerId,
                QuestionId = model.QuestionId,
                QuizTypeId = model.QuizTypeId,
            };
            _quizScoreRepository.Update(quizScore);
            _unitOfWork.Save();
        }
    }
}
