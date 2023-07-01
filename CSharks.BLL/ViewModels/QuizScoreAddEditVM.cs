using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.ViewModels
{
    public class QuizScoreAddEditVM
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionAnswerId { get; set; }
        public int QuizTypeId { get; set; }
    }
}
