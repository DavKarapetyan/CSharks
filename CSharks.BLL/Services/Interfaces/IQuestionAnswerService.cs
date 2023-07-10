using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services.Interfaces
{
    public interface IQuestionAnswerService
    {
        public List<QuestionAnswerVM> GetQuestionAnswers(CultureType cultureType);
        public QuestionAnswerVM GetQuestionAnswer(int id, CultureType cultureType);
        public QuestionAnswerAddEditVM GetForEdit(int id, CultureType cultureType);
        public List<QuestionAnswerVM> GetQuestionAnswersByQuestionId(int questionId, CultureType cultureType);
        public void Add(QuestionAnswerAddEditVM model);
        public void Update(QuestionAnswerAddEditVM model, CultureType cultureType);
        public void Delete(int id);
        public bool IsCorrect(int id);
    }
}
