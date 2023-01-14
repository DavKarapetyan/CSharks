using CSharks.BLL.ViewModels;
using CSharks.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services.Interfaces
{
    internal interface INewsService
    {
        public List<NewsVM> GetAllNews(CultureType cultureType);
        public NewsVM GetNewsById(int id,CultureType cultureType);
        public void Add(NewsVM model);
        public void Update(NewsVM newsVM, CultureType cultureType);
    }
}
