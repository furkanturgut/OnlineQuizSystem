using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Dto_s
{
    public class UserAnswerDto
    {
        public int Id { get; set; }
        public QuestionDto Question { get; set; }
        public OptionDto Option { get; set; }
    }
}
