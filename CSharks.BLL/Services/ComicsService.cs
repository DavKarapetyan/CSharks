using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Repositories;
using CSharks.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services
{
    public class ComicsService : IComicsService
    {   
        private readonly IComicsRepository _comicsRepository;
        private readonly IUnitOfWork _uow;
        public ComicsService(IComicsRepository comicsrepository,IUnitOfWork uow)
        {
            _comicsRepository = comicsrepository;
            _uow = uow;

        }

        public void Add(ComicsVM model)
        {
            Comics comics = new Comics
            {
                Description = model.Description,
                CreateDate = model.CreateDate,
                ImageFile = model.ImageFile,
                Name = model.Name,

            };
            _comicsRepository.Add(comics);
            _uow.Save();
        }

        public void Delete(int Id)
        {
            _comicsRepository.Delete(Id);
            _uow.Save();
        }

        public ComicsVM GetComics(int Id)
        {
            var comics=_comicsRepository.GetById(Id);
            ComicsVM comicsVM= new ComicsVM()
            {
              CreateDate= comics.CreateDate,
              Description=comics.Description,
              ImageFile=comics.ImageFile,
              Name = comics.Name,
            };
            return comicsVM;
        }

        public List<ComicsVM> GetComicses()
        {
            var comics = _comicsRepository.GetAll().Select(c => new ComicsVM
            {
               ImageFile=c.ImageFile,
               Name = c.Name,
               Description= c.Description,
               CreateDate= c.CreateDate,
            }).ToList();
            return comics;
        }

        public ComicsVM GetComicsForEdit(int Id)
        {
            var comics=_comicsRepository.GetForEdit(Id);
            ComicsVM comicsVM = new ComicsVM()
            {
                CreateDate = comics.CreateDate,
                Description = comics.Description,
                ImageFile = comics.ImageFile,
                Name = comics.Name,
            };
            return comicsVM;
        }

        public void Update(ComicsVM model)
        {
            var comics = new Comics()
            {
                ImageFile = model.ImageFile,
                Name = model.Name,
                Description = model.Description,
                CreateDate = model.CreateDate,
            };
            _comicsRepository.Update(comics);
            _uow.Save();
        }
    }
}
