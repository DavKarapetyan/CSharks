using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
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
        private readonly ITranslateService _translateService;
        private readonly IUnitOfWork _uow;
        public ComicsService(IComicsRepository comicsrepository, IUnitOfWork uow, ITranslateService translateService)
        {
            _comicsRepository = comicsrepository;
            _uow = uow;
            _translateService = translateService;
        }

        public void Add(ComicsVM model)
        {
            Comics comics = new Comics()
            {
                DateOfPublication = model.DateOfPublication,
                Description = model.Description,
                ImageFile = model.ImageFile,
                Name = model.Name,
                Price = model.Price,
                NewPrice = model.NewPrice.Value,
                Discount = model.Discount,
            };
            _comicsRepository.Add(comics);
            _uow.Save();
        }

        public void Delete(int Id)
        {
            _comicsRepository.Delete(Id);
            _uow.Save();
        }

        public List<ComicsVM> GetAllComices(CultureType cultureType)
        {
            var comicses = _comicsRepository.GetAll();
            if (cultureType != CultureType.en)
            {
                comicses = _translateService.Convert(comicses, "Comics", 0, cultureType, comicses.Select(c => c.Id).ToList()) as List<Comics>;
            }
            var list = comicses.Select(c => new ComicsVM
            {
                DateOfPublication = c.DateOfPublication,
                Description = c.Description,
                ImageFile = c.ImageFile,
                Name = c.Name,
                Id = c.Id,
                Discount = c.Discount.GetValueOrDefault(),
                NewPrice = c.NewPrice,
                Price = c.Price,
            }).ToList();
            return list;
        }

        public ComicsVM GetComics(int Id, CultureType cultureType)
        {
            var comics = _comicsRepository.GetById(Id);
            if (cultureType != CultureType.en)
            {
                comics = _translateService.Convert(comics, "Comics", Id, cultureType, new List<int> { Id }) as Comics;
            }
            ComicsVM comicsVM = new ComicsVM()
            {
                DateOfPublication = comics.DateOfPublication,
                Description = comics.Description,
                ImageFile = comics.ImageFile,
                Name = comics.Name,
                Id = Id,
                Discount = comics.Discount.GetValueOrDefault(),
                NewPrice = comics.NewPrice,
                Price = comics.Price,
            };
            return comicsVM;
        }

        public ComicsVM GetComicsForEdit(int Id, CultureType cultureType)
        {
            var comics = _comicsRepository.GetForEdit(Id);
            if (cultureType != CultureType.en)
            {
                comics = _translateService.Convert(comics, "Comics", Id, cultureType, new List<int> { Id }) as Comics;
            }
            ComicsVM comicsVM = new ComicsVM()
            {
                Id = Id,
                DateOfPublication = comics.DateOfPublication,
                Description = comics.Description,
                ImageFile = comics.ImageFile,
                Name = comics.Name,
            };
            return comicsVM;
        }

        public void Update(ComicsVM model, CultureType cultureType)
        {
            var entity = _comicsRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.en)
            {
                entity.Description = model.Description;
                entity.Name = model.Name;
                entity.ImageFile = model.ImageFile;
                entity.DateOfPublication = model.DateOfPublication;
                _comicsRepository.Update(entity);
            }
            else
            {
                var tableName = "Comics";
                _translateService.Fill(model, cultureType, tableName, model.Id);
            }
            _uow.Save();
        }
    }
}
