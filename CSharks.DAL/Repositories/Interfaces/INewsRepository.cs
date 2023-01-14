using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories.Interfaces
{
    public interface INewsRepository
    {
        void Add(News model);
        News GetForEdit(int Id);
        void Update(News model);
        News GetById(int Id);
        List<News> GetAll();
        void Delete(int Id);
    }
}
