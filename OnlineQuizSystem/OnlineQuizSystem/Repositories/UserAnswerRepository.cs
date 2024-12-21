using OnlineQuizSystem.Data;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly DataContext _context;

        public UserAnswerRepository(DataContext context)
        {
            this._context = context;
        }

        public bool AnswerExist(int Id)
        {
            return _context.UserAnswers.Any(a => a.Id == Id);
        }

        public bool CreateAnswer(QuizUserAnswer answer)
        {
            _context.Add(answer);
            return Save();
        }

        public bool DeleteAnswer(int Id)
        {
            var answer = GetAnswer(Id);
            _context.Remove(answer);
            return Save();
        }

        public ICollection<QuizUserAnswer> GetAllAnswers()
        {
            var answers = _context.UserAnswers.OrderBy(a => a.Id).ToList();
            return answers;
        }

        public QuizUserAnswer GetAnswer(int Id)
        {
            var answer = _context.UserAnswers.Where(ai => ai.Id == Id).FirstOrDefault();
            return answer;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAnswer(QuizUserAnswer answer)
        {
            _context.Update(answer);
            return Save();
        }
    }
}
