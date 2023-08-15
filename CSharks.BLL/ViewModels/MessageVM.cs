using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.ViewModels
{
    public class MessageVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime When { get; set; }
        public int UserId { get; set; }
    }
}
