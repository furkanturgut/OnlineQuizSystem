using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Models
{
    public class QuestionOption
    {
        public int Id { get; set; }
        [Required]
        public required string Text { get; set; }
        [Required]
        public required int isCorrect { get; set; }
        [Required]
        public QuizQuestion QuizQuestion { get; set; }
        public ICollection<QuizUserAnswer> UserAnswers { get; set; }
    }
}
