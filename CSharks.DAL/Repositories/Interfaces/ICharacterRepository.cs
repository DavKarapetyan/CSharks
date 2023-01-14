using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories.Interfaces
{
    public  interface ICharacterRepository
    {
        void Add(Characters model);
        Characters GetForEdit(int Id);
        void Update(Characters model);
        Characters GetById(int Id);
        List<Characters> GetAll();
        void Delete(int Id);
    }
}
