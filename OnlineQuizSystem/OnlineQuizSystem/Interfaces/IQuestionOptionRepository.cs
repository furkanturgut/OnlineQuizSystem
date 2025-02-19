using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Interfaces
{
    public interface IQuestionOptionRepository
    {
        // create, read, update, delete, exist, save
        bool CreateOption (QuestionOption option);
        QuestionOption GetOption(int Id);
        ICollection<QuestionOption> GetAllOptions();
        ICollection<QuestionOption> GetOptionsOfAQuestion(int QuestionId);

        bool UpdateOption (QuestionOption option);
        bool DeleteOption (int Id);
        bool OptionExist (int Id);
        bool Save();
    }
}
