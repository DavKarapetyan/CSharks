using CSharks.DAL.Entities;
using CSharks.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories
{
    public class QuizTypeRepository : IQuizTypeRepository
    {
        private readonly CSharksDbContext _context;
        public void Add(QuizType quizType)
        {
            _context.QuizTypes.Add(quizType);
        }

        public List<QuizType> GetAll()
        {
            var data = _context.QuizTypes.Select(qt => new QuizType {
                Id= qt.Id,
                Description= qt.Description,
                Title= qt.Title,
            }).AsNoTracking().ToList();
            return data;
        }

        public QuizType GetForEdit(int id)
        {
            var entity = _context.QuizTypes.Where(q => q.Id == id).FirstOrDefault();
            return entity;
        }

        public QuizType GeyById(int id)
        {
            var entity = _context.QuizTypes.Where(q => q.Id == id).AsNoTracking().FirstOrDefault();
            return entity;
        }

        public void Update(QuizType quizType)
        {
            var entity = _context.QuizTypes.FirstOrDefault(qt => qt.Id == quizType.Id);
            entity.Id = quizType.Id;
            entity.Description = quizType.Description;
            entity.Title    = quizType.Title;
        }
    }
}
