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
        public List<QuestionAnswerVM> GetQuestionAnswers();
        public QuestionAnswerVM GetQuestionAnswer(int id);
        public QuestionAnswerAddEditVM GetForEdit(int id);
        public void Add(QuestionAnswerAddEditVM model);
        public void Update(QuestionAnswerAddEditVM model, CultureType cultureType);
    }
}
