namespace OnlineQuizSystem.Models
{
    public class QuestionOption
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required int isCorrect { get; set; }
        public QuizQuestion QuizQuestion { get; set; }
        public ICollection<QuizUserAnswer> UserAnswers { get; set; }
    }
}
