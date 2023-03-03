using CSharks.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.ViewModels
{
    public class CreateUserVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; set; }
        public string NickName { get; set; }
        public Avatar Avatar { get; set; }
    }
}
