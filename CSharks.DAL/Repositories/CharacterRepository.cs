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
    public class CharacterRepository : ICharacterRepository
    {
        private readonly CSharksDbContext _context;
        public CharacterRepository(CSharksDbContext context)
        {
            _context = context;
        }
        public void Add(Characters model)
        {
           _context.Characters.Add(model);
        }

        public void Delete(int Id)
        {
           var data=_context.Characters.Remove(GetById(Id));
        }

        public List<Characters> GetAll()
        {
            var data=_context.Characters.Select(c=>new Characters
            {
                Description= c.Description,
                Name= c.Name,
                Id= c.Id,
                ImageFile= c.ImageFile,
            }).AsNoTracking().ToList();
            return data;
        }

        public Characters GetById(int Id)
        {
            var data=_context.Characters.Where(c=>c.Id==Id).AsNoTracking().FirstOrDefault();
            return data;
        }

        public Characters GetForEdit(int Id)
        {
            var data=_context.Characters.Where(c=>c.Id==Id).FirstOrDefault();
            return data;
        }

        public void Update(Characters model)
        {
           var data=_context.Characters.FirstOrDefault(c=>c.Id==model.Id);
            data.Description= model.Description;    
            data.Name= model.Name;
            data.ImageFile= model.ImageFile;
        }
    }
}
