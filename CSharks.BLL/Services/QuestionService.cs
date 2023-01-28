using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using CSharks.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ITranslateService _translatorService;
        private readonly IUnitOfWork _unitOfWork;
        public QuestionService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork, ITranslateService translatorService)
        {
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
            _translatorService = translatorService;
        }

        public void Add(QuestionAddEditVM model)
        {
            Question question = new Question
            {
                Id = model.Id,
                Text = model.Text,
                QuizTypeId = model.QuizTypeId,
            };
            _questionRepository.Add(question);
            _unitOfWork.Save();
        }

        public QuestionVM GetQuestionById(int id)
        {
            var question = _questionRepository.GetById(id);
            QuestionVM questionVM = new QuestionVM
            {
                Id = id,
                Text = question.Text,
            };
            return questionVM;
        }

        public List<QuestionVM> GetAllQuestion()
        {
            var question = _questionRepository.GetAll().Select(n => new QuestionVM
            {
                Id = n.Id,
                Text = n.Text,
            }).ToList();
            return question;
        }
        public List<Question> GetQuestionByQuizTypeId(int quizTypeId)//stexcel vm
        {
            var questions = _questionRepository.GetAll().Where(q => q.QuizTypeId == quizTypeId).Select(q => new Question 
            {
                Id = q.Id,
                Text = q.Text,
                QuizTypeId = quizTypeId,
                Answers = q.Answers
            }).ToList();
            return questions;
        }
        public void Update(QuestionAddEditVM model, CultureType cultureType)
        {
            var entity = _questionRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.en)
            {
                entity.Text = model.Text;
                entity.QuizTypeId = model.QuizTypeId;
                _questionRepository.Update(entity);
            }
            else
            {
                var tablename = "Questions";
                _translatorService.Fill(model, cultureType, tablename, model.Id);
            }
            _unitOfWork.Save();
        }

        public QuestionAddEditVM GetQuestionForEdit(int id)
        {
            var question = _questionRepository.GetForEdit(id);
            QuestionAddEditVM model = new QuestionAddEditVM
            {
                Id = id,
                QuizTypeId = question.QuizTypeId,
                Text = question.Text,
            };
            return model;
        }

        public void Delete(int id)
        {
            _questionRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
