using CSharks.BLL.ViewModels;
using CSharks.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services.Interfaces
{
    public interface IComicsService
    {
        List<ComicsVM> GetAllComices(CultureType cultureType);
        public ComicsVM GetComics(int Id, CultureType cultureType);
        public ComicsVM GetComicsForEdit(int Id, CultureType cultureType);
        void Add(ComicsVM model);
        void Update(ComicsVM model, CultureType cultureType);
        void Delete(int Id);
    }
}
