using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Dto_s
{
    public class QuizWithAnswersAndOptionsDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public ICollection<QuestionWithOptionDto> QuizQuestions { get; set; }
    }
}
