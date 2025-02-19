using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Dto_s
{
    public class UpdateAnswerDto
    {
        public int Id { get; set; }
        public QuestionOption QuestionOption { get; set; }
    }
}
