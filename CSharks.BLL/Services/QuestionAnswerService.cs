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
    public class QuestionAnswerService : IQuestionAnswerService
    {
        private readonly IQuestionAnswerRepository _questionAnswerRepository;
        private readonly ITranslateService _translatorService;
        private readonly IUnitOfWork _uow;
        public QuestionAnswerService(IQuestionAnswerRepository questionAnswerRepository, IUnitOfWork uow, ITranslateService translatorService)
        {
            _questionAnswerRepository = questionAnswerRepository;
            _uow = uow;
            _translatorService = translatorService;
        }

        public void Add(QuestionAnswerAddEditVM model)
        {
            QuestionAnswer questionAnswer = new QuestionAnswer
            {
                Id = model.Id,
                IsCorrect = model.IsCorrect,
                QuestionId = model.QuestionId,
                Text = model.Text,
            };
            _questionAnswerRepository.Add(questionAnswer);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _questionAnswerRepository.Delete(id);
            _uow.Save();
        }

        public QuestionAnswerAddEditVM GetForEdit(int id, CultureType cultureType)
        {
            var question = _questionAnswerRepository.GetForEdit(id);
            if (cultureType != CultureType.en)
            {
                question = _translatorService.Convert(question, "QuestionAnswers", id, cultureType, new List<int> { id}) as QuestionAnswer;
            }
            QuestionAnswerAddEditVM model = new QuestionAnswerAddEditVM()
            {
                Id = id,
                IsCorrect = question.IsCorrect,
                QuestionId = question.QuestionId,
                Text = question.Text
            };
            return model;
        }

        public QuestionAnswerVM GetQuestionAnswer(int id, CultureType cultureType)
        {
            var questionAnswer = _questionAnswerRepository.GetById(id);
            if (cultureType != CultureType.en)
            {
                questionAnswer = _translatorService.Convert(questionAnswer, "QuestionAnswers", id, cultureType, new List<int> { id}) as QuestionAnswer;
            }
            QuestionAnswerVM question = new QuestionAnswerVM
            {
                Id = id,
                IsCorrect = questionAnswer.IsCorrect,
                Text = questionAnswer.Text,
            };
            return question;
        }

        public List<QuestionAnswerVM> GetQuestionAnswers(CultureType cultureType)
        {
            var questionAnswers = _questionAnswerRepository.GetAll();
            if (cultureType != CultureType.en)
            {
                questionAnswers = _translatorService.Convert(questionAnswers, "QuestionAnswers", 0, cultureType, questionAnswers.Select(qa => qa.Id).ToList()) as List<QuestionAnswer>;
            }
            var list = questionAnswers.Select(n => new QuestionAnswerVM
            {
                Id = n.Id,
                IsCorrect = n.IsCorrect,
                Text = n.Text,
            }).ToList();
            return list;
        }

        public void Update(QuestionAnswerAddEditVM model, CultureType cultureType)
        {
            var entity = _questionAnswerRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.en)
            {
                entity.Text = model.Text;
                entity.IsCorrect = model.IsCorrect;
                entity.QuestionId = model.QuestionId;
                _questionAnswerRepository.Update(entity);
            }
            else
            {
                var tablename = "QuestionAnswers";
                _translatorService.Fill(model, cultureType, tablename, model.Id);
            }
            _uow.Save();
        }
        public bool IsCorrect(int id)
        { 
            var entity = _questionAnswerRepository.GetById(id);
            return entity.IsCorrect;
        }

        public List<QuestionAnswerVM> GetQuestionAnswersByQuestionId(int questionId, CultureType cultureType)
        {
            var questionAnswers = _questionAnswerRepository.GetAll().Where(qa => qa.QuestionId == questionId).ToList();
            if (cultureType != CultureType.en)
            {
                questionAnswers = _translatorService.Convert(questionAnswers, "QuestionAnswers", 0, cultureType, questionAnswers.Select(qa => qa.Id).ToList()) as List<QuestionAnswer>;
            }
            var list = questionAnswers.Select(n => new QuestionAnswerVM
            {
                Id = n.Id,
                IsCorrect = n.IsCorrect,
                Text = n.Text,
            }).ToList();

            return list;
        }
    }
}
