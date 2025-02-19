using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Models
{
    public class QuizUserAnswer
    {
        public int Id { get; set; }
        [Required]
        public required int QuestionId { get; set; }
        [Required]
        public QuizQuestion Question { get; set; }

        public int QuestionOptionId { get; set; }
        [Required]
        public QuestionOption QuestionOption { get; set; }

        [Required]
        public Session session { get; set; }
    }
}
