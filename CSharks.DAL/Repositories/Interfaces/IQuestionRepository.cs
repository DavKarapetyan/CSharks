using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        void Add(Question model);
        Question GetForEdit(int Id);
        void Update(Question model);
        Question GetById(int Id);
        List<Question> GetAll();
        void Delete(int Id);
    }
}
