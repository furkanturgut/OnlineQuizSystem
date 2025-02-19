using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.Dto_s;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;

namespace OnlineQuizSystem.Controllers
{
    public class SessionController : Controller
    {
        private readonly IUserAnswerRepository _answerRepository;
        private readonly IDistributedCache _cache;
        private readonly IQuizRepository _quizRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public SessionController(IUserAnswerRepository answerRepository, IDistributedCache cache, IQuizRepository quizRepository, ISessionRepository sessionRepository, IUserRepository userRepository, IMapper mapper, DataContext context)
        {
            this._answerRepository = answerRepository;
            this._cache = cache;
            this._quizRepository = quizRepository;
            this._sessionRepository = sessionRepository;
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._context = context;
        }

        [HttpPost("start-session")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult StartQuiz(int QuizId, int UserId)
        {
            if (!_quizRepository.QuizExist(QuizId) || !_userRepository.UserExist(UserId))
            {
                return NotFound();
            }
            var quiz = _quizRepository.GetQuiz(QuizId);
            var user = _userRepository.GetUser(UserId);
            var session = new Session()
            {
                Quiz = quiz,
                User = user,
                StartDate = DateTime.Now,
            };
            if (!_sessionRepository.StartQuizSession(session))
            {
                ModelState.AddModelError("", "Something went wrong while starting session");
                return StatusCode(500, ModelState);
            }
            var returnSession = _mapper.Map<SessionDto>(_context.Sessions.OrderByDescending(s => s.Id).FirstOrDefault());
            return Ok(returnSession);


        }

        [HttpPut("end-session/{SessionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public ActionResult EndSession(int SessionId)
        {
            if (!_sessionRepository.SessionExist(SessionId))
            {
                return NotFound();
            }
            var session = _sessionRepository.GetSession(SessionId);
            session.EndDate = DateTime.Now;
            if (!_sessionRepository.EndQuizSession(session))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            var cachedAnswer = _cache.GetString($"session:{SessionId}:answers");
            var answer =JsonConvert.DeserializeObject<List<CreateUserAnswerDto>>(cachedAnswer);

            var userAnswers = answer.Select(a => new QuizUserAnswer
                {
                    QuestionId = a.QuestionId,
                    QuestionOptionId=a.OptionId,
                    session= session,
                }).ToList();
            if(!_answerRepository.CreateAnswer(userAnswers));

            return NoContent();
        }


        /* [HttpPost]
 [ProducesResponseType(204)]
 [ProducesResponseType(400)]
public IActionResult CreateAnswerDb(int QuestionId, int OptionId) 
 {

     CreateUserAnswerDto answer = new CreateUserAnswerDto ();
     var question = _questionRepository.GetQuestion(QuestionId);
     var option= _optionRepository.GetOption(OptionId);
     answer.QuestionOption = option;
     answer.Question= question;
     var AnswerMap= _mapper.Map<QuizUserAnswer>(answer);
     if (!_answerRepository.CreateAnswer(AnswerMap))
     {
         ModelState.AddModelError("", "Something went wrong while saving ");
         return StatusCode(500, ModelState);
     }
     return Ok("Succesfuly");

 } */
    }
}
