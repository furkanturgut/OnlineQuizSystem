using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Dto_s
{
    public class CreateUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
