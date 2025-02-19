using OnlineQuizSystem.Data;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories
{
    public class QuestionOptionRepository : IQuestionOptionRepository
    {
        private readonly DataContext _context;

        public QuestionOptionRepository(DataContext context)
        {
            this._context = context;
        }

        public bool CreateOption(QuestionOption option)
        {
            _context.Add(option);
            return Save();
        }

        public bool DeleteOption(int Id)
        {
            var option = GetOption(Id);
            _context.Remove(option);
            return Save();
        }

        public ICollection<QuestionOption> GetAllOptions()
        {
            var options = _context.QuestionOptions.OrderBy(oi => oi.Id).ToList();
            return options;
        }

        public QuestionOption GetOption(int Id)
        {
            var option = _context.QuestionOptions.Where(qi => qi.Id == Id).FirstOrDefault();
            return option;
        }

        public ICollection<QuestionOption> GetOptionsOfAQuestion(int QuestionId)
        {
            var options = _context.QuestionOptions.Where(qi => qi.QuizQuestion.Id == QuestionId).ToList();
            return options;
        }

        public bool OptionExist(int Id)
        {
            return _context.QuestionOptions.Any(qi => qi.Id == Id);

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOption(QuestionOption option)
        {
            _context.Update(option);
            return Save();
        }
    }
}
