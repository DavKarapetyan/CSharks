using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories.Interfaces
{
    public interface IComicsRepository
    {
        void Add(Comics model);
        Comics GetForEdit(int Id);
        void Update(Comics model);
        Comics GetById(int Id);
        List<Comics> GetAll();
        void Delete(int Id);
    }
}
