using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories.Interfaces
{
    public interface IQuestionAnswerRepository
    {
        void Add(QuestionAnswer model);
        QuestionAnswer GetForEdit(int Id);
        void Update(QuestionAnswer model);
        QuestionAnswer GetById(int Id);
        List<QuestionAnswer> GetAll();
        void Delete(int Id);
    }
}
