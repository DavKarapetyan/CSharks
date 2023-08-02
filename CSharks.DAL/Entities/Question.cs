using System.ComponentModel.DataAnnotations.Schema;
namespace CSharks.DAL.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string? QuestionImage { get; set; }
        public string Explanation { get; set; }
        public virtual ICollection<QuestionAnswer> Answers { get; set; }
        [ForeignKey("QuizType")]
        public int QuizTypeId { get; set; }
        public QuizType QuizType { get; set; }
    }
}