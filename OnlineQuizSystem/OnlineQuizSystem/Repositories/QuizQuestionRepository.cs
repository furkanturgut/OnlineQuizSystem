using OnlineQuizSystem.Data;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Repositories
{
    public class QuizQuestionRepository : IQuizQuestionRepository
    {
        private readonly DataContext _context;

        public QuizQuestionRepository(DataContext context)
        {
            this._context = context;
        }

        public bool CreateQuestion(QuizQuestion question)
        {
            _context.Add(question);
            return Save();
        }

        public bool DeleteQuestion(int Id)
        {
            var question = GetQuestion(Id);
            _context.Remove(question);
            return Save();
        }

        public ICollection<QuizQuestion> GetAllQuestions()
        {
            var questions = _context.QuizQuestions.OrderBy(x => x.Id).ToList();
            return questions;
        }

        public QuizQuestion GetQuestion(int id)
        {
            var question = _context.QuizQuestions.FirstOrDefault(qi => qi.Id == id);
            return question; 
        }

        public bool QuestionExist(int Id)
        {
            return _context.QuizQuestions.Any(qi => qi.Id == Id);

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateQuestion(QuizQuestion question)
        {
            _context.Update(question);
            return Save();
        }
    }
}
