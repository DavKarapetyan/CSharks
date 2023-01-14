using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services.Interfaces
{
    public interface ICharacterService
    {
        List<CharacterVM> GetAllCharacters(CultureType cultureType);
        public CharacterVM GetCharacter(int Id,CultureType cultureType);
        public CharacterVM GetCharacterForEdit(int Id,CultureType cultureType);
        void Add(CharacterVM model);
        void Update(CharacterVM model,CultureType cultureType);
        void Delete(int Id);
    }
}
