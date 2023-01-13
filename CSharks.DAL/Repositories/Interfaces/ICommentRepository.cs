using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharks.DAL.Entities;

namespace CSharks.DAL.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        void Add(Comment model);
        Comment GetForEdit(int Id);
        void Update(Comment model);
        Comment GetById(int Id);
        List<Comment> GetAll();
        void Delete(int Id);
 
    }
}
