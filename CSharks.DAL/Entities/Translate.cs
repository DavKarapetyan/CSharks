using CSharks.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Entities
{
    public class Translate
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string FiledName { get; set; }
        public string Value { get; set; }
        public int PrimaryKey { get; set; }
        public CultureType CultureType { get; set; }

    }
}
