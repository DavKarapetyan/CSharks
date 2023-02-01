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
    public class ComicsRepository : IComicsRepository
    {
        private readonly CSharksDbContext _context;
        public ComicsRepository(CSharksDbContext context)
        {
            _context = context;
        }
        public void Add(Comics model)
        {
            _context.Comics.Add(model);
        }

        public void Delete(int Id)
        {
            var data = _context.Comics.Remove(GetById(Id));
        }

        public List<Comics> GetAll()
        {
            var data = _context.Comics.Select(c => new Comics
            {
                Description = c.Description,
                Name = c.Name,
                Id = c.Id,
                ImageFile = c.ImageFile,
                DateOfPublication = c.DateOfPublication,
                Price = c.Price,
                NewPrice= c.NewPrice,
                Discount = c.Discount,
            }).AsNoTracking().ToList();
            return data;
        }

        public Comics GetById(int Id)
        {
            var data = _context.Comics.Where(c => c.Id == Id).AsNoTracking().FirstOrDefault();
            return data;
        }

        public Comics GetForEdit(int Id)
        {
            var data = _context.Comics.Where(c => c.Id == Id).FirstOrDefault();
            return data;
        }

        public void Update(Comics model)
        {
            var data = _context.Comics.FirstOrDefault(c => c.Id == model.Id);
            data.Description = model.Description;
            data.Name = model.Name;
            data.ImageFile = model.ImageFile;
            data.DateOfPublication = model.DateOfPublication;
            data.Price = model.Price;
            data.NewPrice = model.NewPrice;
            data.Discount = model.Discount;
        }
    }
}
