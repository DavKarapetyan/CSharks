using CSharks.DAL.Entities;
using CSharks.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories
{
    public class TranslateRepository : ITranslateRepository
    {
        private readonly CSharksDbContext _context;
        public TranslateRepository(CSharksDbContext context)
        {
            _context = context;
        }

        public void AddRange(List<Translate> translates)
        {
            _context.Translates.AddRange(translates);
        }

        public List<Translate> List(Expression<Func<Translate, bool>> predicate)
        {
            var data = _context.Translates.Where(predicate).ToList();
            return data;
        }

        public void RemoveRange(List<Translate> translates)
        {
            _context.Translates.RemoveRange(translates);
        }
    }
}
