namespace OnlineQuizSystem.Models
{
    public class QuizUserAnswer
    {
        public int Id { get; set; }
        public QuizQuestion Question { get; set; }
        public QuestionOption QuestionOption { get; set; }
    }
}
