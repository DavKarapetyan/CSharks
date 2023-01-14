using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharks.DAL.Entities;
using CSharks.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CSharks.DAL.Repositories
{
    public class QuestionAnswerRepository : IQuestionAnswerRepository
    {
        private readonly CSharksDbContext _context;
        public QuestionAnswerRepository(CSharksDbContext context)
        {
            _context = context;
        }

        public void Add(QuestionAnswer model)
        {
           _context.QuestionAnswers.Add(model);
        }

        public void Delete(int Id)
        {
            _context.QuestionAnswers.Remove(GetById(Id));
        }

        public List<QuestionAnswer> GetAll()
        {
            var queryAnswer = _context.QuestionAnswers.Select(q => new QuestionAnswer
            {
                Id = q.Id,
                IsCorrect = q.IsCorrect,
                Text = q.Text,
                Question = q.Question,
                QuestionId = q.QuestionId,

            }).AsNoTracking().ToList();
            return queryAnswer;
        }

        public QuestionAnswer GetById(int Id)
        {
            var queryAnswer=_context.QuestionAnswers.Where(q=> q.Id == Id).AsNoTracking().FirstOrDefault();
            return queryAnswer;
        }

        public QuestionAnswer GetForEdit(int Id)
        {
            var queryAnswer = _context.QuestionAnswers.Where(q => q.Id == Id).FirstOrDefault();
            return queryAnswer;
        }

        public void Update(QuestionAnswer model)
        {
            var queryAnswer=_context.QuestionAnswers.FirstOrDefault(q=> q.Id == model.Id);
            queryAnswer.IsCorrect=model.IsCorrect;
            queryAnswer.Text=model.Text;
            queryAnswer.Question=model.Question;
            queryAnswer.QuestionId=model.QuestionId;
        }
    }
}
