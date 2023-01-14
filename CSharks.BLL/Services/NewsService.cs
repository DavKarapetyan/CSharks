using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using CSharks.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly ITranslateService _translateService;
        private readonly IUnitOfWork _uow;
        public NewsService(INewsRepository newsRepository, ITranslateService translateRepository, IUnitOfWork uow)
        {
            _newsRepository = newsRepository;
            _translateService = translateRepository;
            _uow = uow;
        }

        public void Add(NewsVM model)
        {
            News news = new News
            {
                Description = model.Description,
                ImageFile = model.ImageFile,
                NewsType = model.NewsType,
                Title = model.Title,
            };
            _newsRepository.Add(news);
            _uow.Save();
        }

        public void Delete(int Id)
        {
            _newsRepository.Delete(Id);
            _uow.Save();
        }

        public List<NewsVM> GetAllNews(CultureType cultureType)
        {
            var news = _newsRepository.GetAll();
            if (cultureType != CultureType.en)
            {
                news = _translateService.Convert(news, "News", 0, cultureType, news.Select(n => n.Id).ToList()) as List<News>;
            }
            var list = news.Select(n => new NewsVM
            {
                Description = n.Description,
                ImageFile = n.ImageFile,
                NewsType = n.NewsType,
                Id = n.Id,
                Title = n.Title,
            }).ToList();
            return list;
        }

        public NewsVM GetNewsById(int id, CultureType cultureType)
        {
            var news = _newsRepository.GetById(id);
            if (cultureType != CultureType.en)
            {
                news = _translateService.Convert(news, "News", id, cultureType, new List<int> { id }) as News;
            }
            NewsVM newsVM = new NewsVM
            {
                Id = id,
                Description = news.Description,
                ImageFile = news.ImageFile,
                NewsType = news.NewsType,
                Title = news.Title,
            };
            return newsVM;
        }

        public void Update(NewsVM newsVM, CultureType cultureType)
        {
            var entity = _newsRepository.GetForEdit(newsVM.Id);
            if (cultureType == CultureType.en)
            {
                entity.Title = newsVM.Title;
                entity.Description = newsVM.Description;
                entity.NewsType = newsVM.NewsType;
                entity.ImageFile = newsVM.ImageFile;
                _newsRepository.Update(entity);
            }
            else
            {
                var tableName = "News";
                _translateService.Fill(newsVM, cultureType, tableName, newsVM.Id);
            }
            _uow.Save();
        }
    }
}
