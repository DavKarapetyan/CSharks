using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Entities
{
    public class Comics
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string ImageFile { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
    }
}
