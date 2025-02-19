using Microsoft.EntityFrameworkCore;
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

        public bool CreateAnswer(List<QuizUserAnswer> answers)
        {
            _context.AddRange(answers);
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
            var answers = _context.UserAnswers
            .Include(ua => ua.Question)
            .Include(ua => ua.QuestionOption)
            .ToList();
            return answers;
        }

        public QuizUserAnswer GetAnswer(int Id)
        {
            var answer = _context.UserAnswers.Include(ua => ua.Question).Include(ua => ua.QuestionOption).FirstOrDefault();
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
