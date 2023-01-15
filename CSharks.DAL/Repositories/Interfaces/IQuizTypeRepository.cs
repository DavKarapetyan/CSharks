using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories.Interfaces
{
    public interface IQuizTypeRepository
    {
        void Add(QuizType quizType);
        QuizType GetForEdit(int id);
        void Update(QuizType quizType);
        QuizType GetById(int id);
        List<QuizType> GetAll();
        void Delete(int Id);

    }
}
