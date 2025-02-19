using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Dto_s
{
    public class CreateUserAnswerDto
    {
        public int QuestionId { get; set; }
        public int  OptionId { get; set; }
    }
}
