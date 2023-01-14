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
        public List<QuestionVM> GetAllQuestion();
        public QuestionVM GetQuestionById(int id);
        public QuestionAddEditVM GetQuestionForEdit(int id);
        public void Add(QuestionAddEditVM model);
        public void Update(QuestionAddEditVM model, CultureType cultureType);
    }
}
