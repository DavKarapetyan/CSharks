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
    public class QuestionRepository : IQuestionRepository
    { 
        private readonly CSharksDbContext _context;
        public QuestionRepository(CSharksDbContext context)
        {
            _context = context;
        }

        public void Add(Question model)
        {
            _context.Questions.Add(model);
        }

        public void Delete(int Id)
        {
            var query=_context.Questions.Remove(GetById(Id));
        }

        public List<Question> GetAll()
        {
           var query=_context.Questions.Select(q=>new Question
           {
               QuizType= q.QuizType,
               Answers= q.Answers,
               Id=q.Id,
               QuizTypeId=q.QuizTypeId,
               Text=q.Text,
   
           }).AsNoTracking().ToList();
            return query;
        }

        public Question GetById(int Id)
        {
           var query=_context.Questions.Where(q=>q.Id==Id).AsNoTracking().FirstOrDefault();
            return query;
        }

        public Question GetForEdit(int Id)
        {
            var query = _context.Questions.Where(q => q.Id == Id).FirstOrDefault();
            return query;
        }

        public void Update(Question model)
        {
           var query=_context.Questions.FirstOrDefault(q=>q.Id==model.Id);
            query.Answers=model.Answers;
            query.Text=model.Text;
            query.QuizType=model.QuizType;
            query.QuizTypeId=model.QuizTypeId;
        }
    }
}
