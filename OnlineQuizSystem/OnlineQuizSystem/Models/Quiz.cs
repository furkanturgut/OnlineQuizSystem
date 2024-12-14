namespace OnlineQuizSystem.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public ICollection<QuizQuestion> QuizQuestions { get; set; }
    }
}
