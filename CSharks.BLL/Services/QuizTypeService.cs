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
    public class QuizTypeService : IQuizTypeService
    {
        private readonly IQuizTypeRepository _quizTypeRepository;
        private readonly ITranslateService _translatorService;
        private readonly IUnitOfWork _uow;
        public QuizTypeService(IQuizTypeRepository quizTypeRepository, IUnitOfWork uow, ITranslateService translatorService)
        {
            _quizTypeRepository = quizTypeRepository;
            _uow = uow;
            _translatorService = translatorService;
        }

        public void Add(QuizTypeVM model)
        {
            QuizType quiztype = new QuizType
            {
                Title = model.Title,
                Description = model.Description,
            };
            _quizTypeRepository.Add(quiztype);
            _uow.Save();
        }

        public void Delete(int id)
        {
            _quizTypeRepository.Delete(id);
            _uow.Save();
        }

        public QuizTypeVM GetQuizTypeById(int id)
        {
            var quiz = _quizTypeRepository.GetById(id);
            QuizTypeVM quizVM = new QuizTypeVM
            {
                Id = id,
                Description = quiz.Description,
                Title = quiz.Title,
            };
            return quizVM;
        }

        public List<QuizTypeVM> GetQuizTypes()
        {
            var quiz = _quizTypeRepository.GetAll().Select(n => new QuizTypeVM
            {
                Id = n.Id,
                Description = n.Description,
                Title = n.Title,
            }).ToList();
            return quiz;
        }

        public void Update(QuizTypeVM model, CultureType cultureType)
        {
            var entity = _quizTypeRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.en)
            {
                entity.Description = model.Description;
                entity.Title = model.Title;
                _quizTypeRepository.Update(entity);
            }
            else
            {
                var tablename = "QuizTypes";
                _translatorService.Fill(model, cultureType, tablename, model.Id);
            }
            _uow.Save();
            //var quiz = new QuizType
            //{
            //    Id = model.Id,
            //    Description = model.Description,
            //    Title = model.Title,
            //};
            //_quizTypeRepository.Update(quiz);
            //_uow.Save();
        }
    }
}
