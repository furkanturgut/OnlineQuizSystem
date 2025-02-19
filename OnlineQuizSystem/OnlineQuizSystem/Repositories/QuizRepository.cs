using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;
using System.Linq;

namespace OnlineQuizSystem.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly DataContext _context;

        public QuizRepository(DataContext context)
        {
            this._context = context;
        }

        public bool CreateQuiz(Quiz quiz)
        {
            _context.Add(quiz);
            return Save();
        }

        public bool DeleteQuiz(int Id)
        {
            var quiz = GetQuiz(Id);
            _context.Remove(quiz);
            return Save();
        }

      

        public ICollection<Quiz> GetAllQuiz()
        {
            var quizzes = _context.Quizzes.OrderBy(x => x.Id).ToList();
            return quizzes;
        }

        public Quiz GetQuiz(int Id)
        {
            var quiz = _context.Quizzes.Where(qi => qi.Id == Id).FirstOrDefault();
            return quiz;
        }

        public Quiz GetQuizWithQuestionsAndOptions(int QuizId)
        {
            var quiz = _context.Quizzes.
                Include(q => q.QuizQuestions).
                ThenInclude(q => q.QuestionOptions).
                FirstOrDefault(qi => qi.Id == QuizId);
            return quiz;
        }

      

   

        public bool QuizExist(int Id)
        {
            return _context.Quizzes.Any(qi => qi.Id == Id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

      

        public bool UpdateQuiz(Quiz quiz)
        {
            _context.Update(quiz);
            return Save();
        }
    }
}
