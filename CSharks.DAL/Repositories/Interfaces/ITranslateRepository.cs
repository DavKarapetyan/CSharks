using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories.Interfaces
{
    public interface ITranslateRepository
    {
        List<Translate> List(Expression<Func<Translate, bool>> predicate);
        void AddRange(List<Translate> translates);
        void RemoveRange(List<Translate> translates);
    }
}
