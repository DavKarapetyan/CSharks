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

        public QuestionAnswerAddEditVM GetForEdit(int id)
        {
            var question = _questionAnswerRepository.GetForEdit(id);
            QuestionAnswerAddEditVM model = new QuestionAnswerAddEditVM()
            {
                Id = id,
                IsCorrect = question.IsCorrect,
                QuestionId = question.QuestionId,
                Text = question.Text
            };
            return model;
        }

        public QuestionAnswerVM GetQuestionAnswer(int id)
        {
            var questionAnswer = _questionAnswerRepository.GetById(id);
            QuestionAnswerVM question = new QuestionAnswerVM
            {
                Id = id,
                IsCorrect = questionAnswer.IsCorrect,
                Text = questionAnswer.Text,
            };
            return question;
        }

        public List<QuestionAnswerVM> GetQuestionAnswers()
        {
            var questionAnswers = _questionAnswerRepository.GetAll().Select(n => new QuestionAnswerVM
            {
                Id = n.Id,
                IsCorrect = n.IsCorrect,
                Text = n.Text,
            }).ToList();
            return questionAnswers;
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
            //var questionAnswer = new QuestionAnswer
            //{
            //    Id = model.Id,
            //    Text = model.Text,
            //    IsCorrect= model.IsCorrect,
            //    QuestionId= model.QuestionId,
            //};
            //_questionAnswerRepository.Update(questionAnswer);
            //_uow.Save();
        }
    }
}
