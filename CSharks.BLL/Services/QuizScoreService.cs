using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
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
        private readonly IUnitOfWork _unitOfWork;
        public QuizScoreService(IQuizScoreRepository quizScoreRepository, IUnitOfWork uow, IQuestionService questionService, IQuestionAnswerService questionAnswer, IQuizTypeService quizTypeService)
        {
            _quizScoreRepository = quizScoreRepository;
            _unitOfWork = uow;
            _questionService = questionService;
            _questionAnswerService = questionAnswer;
            _quizTypeService = quizTypeService;
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

        public QuizScoreVM GetQuizScoreById(int id)
        {
            var quizScore = _quizScoreRepository.GetById(id);
            QuizScoreVM quizScoreVM = new QuizScoreVM
            {
                Id = id,
                QuestionAnswer = _questionAnswerService.GetQuestionAnswer(quizScore.QuestionAnswerId),
                Score = quizScore.Score,
                UserId = quizScore.UserId,
                Question = _questionService.GetQuestionById(quizScore.QuestionId),
                QuizType = _quizTypeService.GetQuizTypeById(quizScore.QuizTypeId)
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
                Question = _questionService.GetQuestionById(n.QuestionId),
                QuestionAnswer = _questionAnswerService.GetQuestionAnswer(n.QuestionAnswerId),
                QuizType = _quizTypeService.GetQuizTypeById(n.QuizTypeId),
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
