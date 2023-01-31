﻿using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.ViewModels
{
    public class QuestionVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? QuizTypeId { get; set; }
        public List<QuestionAnswer> Answers { get; set; }
    }
}
