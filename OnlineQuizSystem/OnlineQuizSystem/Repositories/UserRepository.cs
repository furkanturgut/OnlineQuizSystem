using OnlineQuizSystem.Data;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            this._context = context;
        }
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(int id)
        {
            var user= GetUser(id);
            _context.Remove(user);
            return Save();
        }

        public User GetUser(int id)
        {
            var user = _context.Users.Where(ui =>  ui.Id == id).FirstOrDefault();
            return user;
        }

        public ICollection<User> GetUsers()
        {
            var users = _context.Users.OrderBy(ui => ui.Id).ToList();
            return users;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExist(int id)
        {
            return _context.Users.Any(ui => ui.Id == id);
        }
    }
}
