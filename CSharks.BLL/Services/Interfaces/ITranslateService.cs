using CSharks.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services.Interfaces
{
    public interface ITranslateService
    {
        object Convert(object model,string tableName,int primaryKey, CultureType cultureType, List<int> primaryKeys);
        void Fill(object model, CultureType cultureType,string tableName, int primaryKey);
    }
}
