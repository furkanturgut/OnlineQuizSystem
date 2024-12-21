using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Interfaces
{
    public interface IQuizRepository
    {
        bool CreateQuiz (Quiz quiz);
        bool UpdateQuiz (Quiz quiz);
        bool DeleteQuiz (int Id);
        Quiz GetQuiz (int Id);
        ICollection<Quiz> GetAllQuiz ();

        bool QuizExist (int Id);
        bool Save();

    }
}
