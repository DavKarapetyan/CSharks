using CSharks.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services.Interfaces
{
    public interface IQuizScoreService
    {
        List<QuizScoreVM> GetQuizScores();
        QuizScoreVM GetQuizScoreById(int id);
        QuizScoreAddEditVM GetQuizScoreForEdit(int id);
        void Add(QuizScoreAddEditVM model);
        void Update(QuizScoreAddEditVM model);
        void Delete(int id);
    }
}
