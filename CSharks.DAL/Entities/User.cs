using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Entities
{
    public class User:IdentityUser<int>
    {
        public DateTime DOB { get; set; }
        public string NickName { get; set; }
        public string AvatarImage { get; set; }
    }
}
