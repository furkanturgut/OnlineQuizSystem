using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Interfaces
{
    public interface IUserRepository
    {
        bool CreateUser(User user); 
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool UserExist(int id);
        bool UpdateUser (User user);

        bool DeleteUser (int id);
        bool Save();
    }
}
