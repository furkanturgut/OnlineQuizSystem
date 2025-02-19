using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystem.Models
{
    public class User
    {
        public  int Id  { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string  LastName { get; set; }
        public string Email { get; set; }
        public int score { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
