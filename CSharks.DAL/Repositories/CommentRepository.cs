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
    public class CommentRepository : ICommentRepository
    {
        private readonly CSharksDbContext _context;
        public CommentRepository(CSharksDbContext context)
        {
            _context = context;
        }

        public void Add(Comment model)
        {
            _context.Comments.Add(model);
        }

        public void Delete(int Id)
        {
            var data=_context.Comments.Remove(GetById(Id));
        }

        public List<Comment> GetAll()
        {
            var data=_context.Comments.Select(c=>new Comment
            {
                Id=c.Id,
                Name=c.Name,
                CommentText=c.CommentText,
                DateOfComment=c.DateOfComment,
                Email=c.Email,
                News=c.News,
                NewsId=c.NewsId,
            }).AsNoTracking().ToList();
            return data;
        }

        public Comment GetById(int Id)
        {
            var entity=_context.Comments.Where(c=>c.Id==Id).AsNoTracking().FirstOrDefault();
            return entity;
        }

        public Comment GetForEdit(int id)
        {
            var data=_context.Comments.Where(c => c.Id==id).FirstOrDefault();
            return data;
        }

        public void Update(Comment model)
        {
            var data=_context.Comments.FirstOrDefault(c=>c.Id==model.Id);
            data.Name=model.Name;
            data.CommentText=model.CommentText;
            data.Email=model.Email;
            data.News=model.News;
            data.NewsId=model.NewsId;
            data.DateOfComment=model.DateOfComment;
        }
    }
    
}
