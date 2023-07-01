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
    public class QuizScoreRepository : IQuizScoreRepository
    {
        private readonly CSharksDbContext _context;
        public QuizScoreRepository(CSharksDbContext context)
        {
            _context = context;
        }
        public void Add(QuizScore model)
        {
            _context.QuizScores.Add(model);
        }

        public void Delete(int id)
        {
            _context.QuizScores.Remove(GetById(id));
        }

        public List<QuizScore> GetAll()
        {
            var data = _context.QuizScores.Select(qs => new QuizScore { 
                Id = qs.Id,
                Score = qs.Score,
                UserId = qs.UserId,
                QuestionAnswerId = qs.QuestionAnswerId,
                QuestionId = qs.QuestionId,
                QuizTypeId = qs.QuizTypeId,
            }).AsNoTracking().ToList();

            return data;
        }

        public QuizScore GetById(int id)
        {
            var entity = _context.QuizScores.Where(qs => qs.Id == id).AsNoTracking().FirstOrDefault();
            return entity;
        }

        public QuizScore GetForEdit(int id)
        {
            var entity = _context.QuizScores.Where(qs => qs.Id == id).FirstOrDefault();
            return entity;
        }

        public void Update(QuizScore model)
        {
            var entity = _context.QuizScores.FirstOrDefault(qs => qs.Id == model.Id);
            entity.Score = model.Score;
            entity.UserId = model.UserId;
            entity.QuestionAnswerId = model.QuestionAnswerId;
            entity.QuestionId = model.QuestionId;
            entity.QuizTypeId = model.QuizTypeId;
        }
    }
}
