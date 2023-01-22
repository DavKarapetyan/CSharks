using CSharks.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.ViewModels
{
    public class NewsVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public NewsType NewsType { get; set; }
        public CultureType CultureType { get; set; }
        public DateTime CreateDate { get; set; }    
    }
}
