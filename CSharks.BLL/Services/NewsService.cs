using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using CSharks.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
                CreateDate = DateTime.Now
            };
            _newsRepository.Add(news);
            _uow.Save();
        }

        public void Delete(int Id)
        {
            _newsRepository.Delete(Id);
            _uow.Save();
        }

        public List<NewsVM> GetAllNews(CultureType cultureType, string? text, NewsType? newsType)
        {
            var news = _newsRepository.GetAll();
            if (cultureType != CultureType.en)
            {
                news = _translateService.Convert(news, "News", 0, cultureType, news.Select(n => n.Id).ToList()) as List<News>;
            }
            var baseQuery = _newsRepository.GetAll().Where(n => (
            (text == null || n.Title.ToLower().Contains(text.ToLower()))
                       && (newsType == null || n.NewsType == newsType)));

            var list = baseQuery.Select(n => new NewsVM
            {
                Description = n.Description,
                ImageFile = n.ImageFile,
                NewsType = n.NewsType,
                Title = n.Title,
                Id = n.Id,
                CreateDate = n.CreateDate
            }).ToList();
            return list;
        }

        public NewsVM GetNewsById(int id, CultureType cultureType)
        {
            var news = _newsRepository.GetForEdit(id);
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
                CreateDate= news.CreateDate,
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
                entity.CreateDate= newsVM.CreateDate;
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
