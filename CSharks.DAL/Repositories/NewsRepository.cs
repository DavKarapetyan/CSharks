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
    public class NewsRepository : INewsRepository
    {
        private readonly CSharksDbContext _context;
        public NewsRepository(CSharksDbContext context)
        {
            _context = context;
        }
        public void Add(News model)
        {
           _context.News.Add(model);
        }

        public void Delete(int Id)
        {
            var data = _context.News.Remove(GetById(Id));
        }

        public List<News> GetAll()
        {
           var data = _context.News.Select(n => new News
           {
               Id = n.Id,
               Comments= n.Comments,
               Description= n.Description,
               ImageFile= n.ImageFile,
               NewsType= n.NewsType,
               Title= n.Title,
           }).AsNoTracking().ToList();  
            return data;    
        }

        public News GetById(int Id)
        {
           var data=_context.News.Where (n => n.Id == Id).AsNoTracking().FirstOrDefault();
            return data;
        }

        public News GetForEdit(int Id)
        {
           var data=_context.News.Where(n=>n.Id== Id).FirstOrDefault();
            return data;
        }

        public void Update(News model)
        {
           var data=_context.News.FirstOrDefault(n=>n.Id== model.Id);
            data.Comments = model.Comments;
            data.Description = model.Description;
            data.ImageFile = model.ImageFile;
            data.NewsType = model.NewsType;
            data.Title = model.Title;   
            

        }
    }
}
