using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Enums
{
    public enum CultureType
    {
        [Display(Name ="Հայ")]
        am=1,
        [Display(Name = "Рус")]
        ru,
        [Display(Name = "En")]
        en=3,

    }
}
