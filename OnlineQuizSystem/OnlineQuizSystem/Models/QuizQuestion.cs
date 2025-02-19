using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        [Required]
        public required string Text { get; set; }
        [Required]
        public required Quiz Quiz { get; set; }
        public required ICollection<QuestionOption> QuestionOptions { get; set; }
        public required ICollection<QuizUserAnswer> UserAnswers { get; set; }


    }
}
