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
    public interface IQuestionService
    {
        public List<QuestionVM> GetAllQuestion(CultureType cultureType);
        public QuestionVM GetQuestionById(int id, CultureType cultureType);
        public QuestionAddEditVM GetQuestionForEdit(int id, CultureType cultureType);
        public List<QuestionVM> GetQuestionByQuizTypeId(int quizTypeId, CultureType cultureType);
        public void Add(QuestionAddEditVM model);
        public void Update(QuestionAddEditVM model, CultureType cultureType);
        public void Delete(int id);
    }
}
