namespace OnlineQuizSystem.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required Quiz Quiz { get; set; }
        public required ICollection<QuestionOption> QuestionOptions { get; set; }
        public required ICollection<QuizUserAnswer> UserAnswers { get; set; }


    }
}
