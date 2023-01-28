﻿using CSharks.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.ViewModels
{
    public class ComicsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string ImageFile { get; set; }
        public string Description { get; set; }
        public CultureType CultureType { get; set; }
        public int Price { get; set; }
        public bool HaveDiscount { get; set; }
        public double NewPrice { get; set; }
        public int Discount { get; set; }
    }
}
