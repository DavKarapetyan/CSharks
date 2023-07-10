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
        private readonly IQuestionAnswerService _questionAnswerService;
        private readonly ITranslateService _translatorService;
        private readonly IUnitOfWork _unitOfWork;
        public QuestionService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork,IQuestionAnswerService questionAnswerService,ITranslateService translatorService)
        {
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
            _questionAnswerService = questionAnswerService;
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

        public QuestionVM GetQuestionById(int id, CultureType cultureType)
        {
            var question = _questionRepository.GetById(id);
            if (cultureType != CultureType.en)
            {
                question = _translatorService.Convert(question, "Questions", id, cultureType, new List<int> { id}) as Question;
            }
            QuestionVM questionVM = new QuestionVM
            {
                Id = id,
                Text = question.Text,
            };
            return questionVM;
        }

        public List<QuestionVM> GetAllQuestion(CultureType cultureType)
        {
            var question = _questionRepository.GetAll();
            if (cultureType != CultureType.en)
            {
                question = _translatorService.Convert(question, "Questions", 0, cultureType, question.Select(q => q.Id).ToList()) as List<Question>;
            }
            var list = question.Select(n => new QuestionVM
            {
                Id = n.Id,
                Text = n.Text,
            }).ToList();
            return list;
        }
        public List<QuestionVM> GetQuestionByQuizTypeId(int quizTypeId, CultureType cultureType)
        {
            var questions = _questionRepository.GetAll();
            if (cultureType != CultureType.en)
            {
                questions = _translatorService.Convert(questions, "Questions", 0, cultureType, questions.Select(q => q.Id).ToList()) as List<Question>;
            }
            var list = questions.Where(q => q.QuizTypeId == quizTypeId).Select(q => new QuestionVM
            {
                Id = q.Id,
                Text = q.Text,
                QuizTypeId = quizTypeId,
                Answers = _questionAnswerService.GetQuestionAnswersByQuestionId(q.Id, cultureType).OrderBy(a => Guid.NewGuid()).ToList()
            }).ToList();
            return list;
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

        public QuestionAddEditVM GetQuestionForEdit(int id, CultureType cultureType)
        {
            var question = _questionRepository.GetForEdit(id);
            if (cultureType != CultureType.en)
            {
                question = _translatorService.Convert(question, "Questions", id, cultureType, new List<int> { id}) as Question;
            }
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
