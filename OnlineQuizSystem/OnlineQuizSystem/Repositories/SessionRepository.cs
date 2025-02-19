using Microsoft.EntityFrameworkCore;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DataContext _context;

        public SessionRepository(DataContext context)
        {
            this._context = context;
        }
        public bool EndQuizSession(Session session)
        {
            _context.Update(session);
            return Save();
        }

        public Session GetSession(int Id)
        {
            return _context.Sessions.FirstOrDefault(s => s.Id == Id);
        }

        public bool isSessionEnd(int Id)
        {
            var session = GetSession(Id);
            if (session.EndDate == null)
            {
                return false;
            }
            return true;
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
            return saved > 0 ? true : false;

        }

        public bool SessionExist(int Id)
        {
            return _context.Sessions.Any(s => s.Id == Id);
        }

        public bool StartQuizSession(Session session)
        {
            _context.Add(session);
            return Save();
        }
    }
}
