using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Dto_s
{
    public class UpdateUserScoreDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Score { get; set; }
    }
}
