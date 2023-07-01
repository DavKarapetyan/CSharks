using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.ViewModels
{
    public class QuizScoreVM
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int UserId { get;set; }
        public QuestionVM Question { get; set; }
        public QuestionAnswerVM QuestionAnswer { get; set; }
        public QuizTypeVM QuizType { get; set; }
    }
}
