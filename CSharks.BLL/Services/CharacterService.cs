using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using CSharks.DAL.Repositories;
using CSharks.DAL.Repositories.Interfaces;

namespace CSharks.BLL.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ITranslateService _translateService;
        private readonly IUnitOfWork _uow;
        public CharacterService(ICharacterRepository characterRepository, IUnitOfWork uow,ITranslateService translateService)
        {
            _characterRepository = characterRepository;
            _uow = uow;
            _translateService = translateService;
        }

        public void Add(CharacterVM model)
        {
            Characters characters = new Characters
            {
                Description = model.Description,
                ImageFile = model.ImageFile,
                Name = model.Name,
            };
            _characterRepository.Add(characters);
            _uow.Save();
        }

        public void Delete(int Id)
        {
            _characterRepository.Delete(Id);
            _uow.Save();

        }

        public CharacterVM GetCharacter(int Id,CultureType cultureType)
        {
            var character = _characterRepository.GetById(Id);
            if (cultureType != CultureType.en)
            { 
                character = _translateService.Convert(character, "Characters", Id, cultureType, new List<int> { Id }) as Characters;
            }
            CharacterVM characterVM = new CharacterVM()
            { 
                Id = Id,
                Description= character.Description,
                ImageFile= character.ImageFile,
                Name = character.Name,
            };
            return characterVM;
        }

        public CharacterVM GetCharacterForEdit(int Id,CultureType cultureType)
        {
            var character = _characterRepository.GetForEdit(Id);
            if (cultureType != CultureType.en)
            {
                character = _translateService.Convert(character, "Characters", Id, cultureType, new List<int> { Id }) as Characters;
            }
            CharacterVM characterVM = new CharacterVM()
            { 
                Id= Id,
                Description = character.Description,
                ImageFile = character.ImageFile,
                Name = character.Name,
            };
            return characterVM;
        }

        public List<CharacterVM> GetAllCharacters(CultureType cultureType)
        {
            var characters = _characterRepository.GetAll();
            if (cultureType != CultureType.en)
            {
                characters = _translateService.Convert(characters, "Characters", 0,cultureType,characters.Select(c => c.Id).ToList()) as List<Characters>;
            }
            var list = characters.Select(c => new CharacterVM
            {
                Id = c.Id,
                Description = c.Description,
                ImageFile = c.ImageFile,
                Name = c.Name,
            }).ToList();
            return list;
        }

        public void Update(CharacterVM model,CultureType cultureType)
        {
            var entity = _characterRepository.GetForEdit(model.Id);
            if (cultureType == CultureType.en)
            {
                entity.Description = model.Description;
                entity.Name = model.Name;
                entity.ImageFile = model.ImageFile;
                _characterRepository.Update(entity);
            }
            else 
            {
                var tableName = "Characters";
                _translateService.Fill(model,cultureType,tableName,model.Id);
            }
            _uow.Save();
        }
    }
}
