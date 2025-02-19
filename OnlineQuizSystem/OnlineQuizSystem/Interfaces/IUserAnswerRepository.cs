using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Interfaces
{
    public interface IUserAnswerRepository
    {
        // create, read, update, delete, exist, save

        bool CreateAnswer(List<QuizUserAnswer> answers);
        QuizUserAnswer GetAnswer (int Id);
        ICollection<QuizUserAnswer> GetAllAnswers ();
        bool UpdateAnswer (QuizUserAnswer answer);
        bool DeleteAnswer (int Id);
        bool AnswerExist (int Id);
        bool Save();

    }
}
