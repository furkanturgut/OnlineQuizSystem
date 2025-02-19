using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Models
{
    public class Session
    {
        public  int Id { get; set; }

        [Required]
        public required DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
        public int Score { get; set; }

        [Required]
        public required Quiz Quiz { get; set; }

        [Required]
        public required User User { get; set; }

        public ICollection<QuizUserAnswer> Answers { get; set; }
    }
}
