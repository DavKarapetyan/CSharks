using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Entities
{
    public class QuizScore
    {
        public int Id { get; set; }
        public int Score { get;set; }
        public int UserId { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        [ForeignKey("QuestionAnswer")]
        public int QuestionAnswerId { get; set; }
        public QuestionAnswer QuestionAnswer { get; set;}

        [ForeignKey("QuizType")]
        public int QuizTypeId { get; set; }
        public QuizType QuizType { get; set; }
    }
}
