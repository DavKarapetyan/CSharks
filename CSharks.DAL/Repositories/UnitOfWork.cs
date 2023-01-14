using CSharks.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CSharksDbContext _context;
        public UnitOfWork(CSharksDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
