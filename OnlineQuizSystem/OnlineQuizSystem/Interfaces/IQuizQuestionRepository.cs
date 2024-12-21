using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Interfaces
{
    public interface IQuizQuestionRepository
    {
        bool CreateQuestion (QuizQuestion question);
        ICollection<QuizQuestion> GetAllQuestions();
        QuizQuestion GetQuestion (int id);
        bool UpdateQuestion (QuizQuestion question);
        bool DeleteQuestion (int Id);
        bool Save();
        bool QuestionExist(int Id);
    }
}
