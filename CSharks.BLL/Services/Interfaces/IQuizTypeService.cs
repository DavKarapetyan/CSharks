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
    public interface IQuizTypeService
    {
        public List<QuizTypeVM> GetQuizTypes();
        public QuizTypeVM GetQuizTypeById(int id);
        public void Add(QuizTypeVM model);
        public void Update(QuizTypeVM model, CultureType cultureType);
        public void Delete(int id);
    }
}
