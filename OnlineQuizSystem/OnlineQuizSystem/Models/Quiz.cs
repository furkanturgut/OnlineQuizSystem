using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
