using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Interfaces
{
    public interface ISessionRepository
    {
        bool StartQuizSession(Session session);
        bool EndQuizSession(Session session);
        Session GetSession(int Id);
        bool SessionExist(int Id);
        bool isSessionEnd(int Id);
        bool Save();
    }
}
