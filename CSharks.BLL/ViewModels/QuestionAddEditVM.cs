using CSharks.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.ViewModels
{
    public class QuestionAddEditVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuizTypeId { get; set; }
        public CultureType Culture { get; set; }
    }
}
