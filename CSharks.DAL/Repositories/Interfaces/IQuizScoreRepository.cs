using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories.Interfaces
{
    public interface IQuizScoreRepository
    {
        void Add(QuizScore model);
        QuizScore GetForEdit(int id);
        void Update(QuizScore model);
        QuizScore GetById(int id);
        List<QuizScore> GetAll();
        void Delete(int id);
    }
}
