using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Dto_s
{
    public class QuestionWithOptionDto
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required ICollection<OptionDto> QuestionOptions { get; set; }
    }
}
