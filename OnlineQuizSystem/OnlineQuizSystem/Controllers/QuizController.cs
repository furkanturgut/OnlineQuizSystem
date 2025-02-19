using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.Dto_s;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;
using System.Collections.Generic;

namespace OnlineQuizSystem.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class QuizController : Controller
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;

        public QuizController(IQuizRepository repository, IMapper mapper, IUserRepository userRepository, DataContext context)
        {
            this._quizRepository = repository;
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._context = context;
        }

        [HttpGet("Get-All")]
        [ProducesResponseType(200, Type = typeof(ICollection<QuizDto>))]
        public ActionResult<ICollection<QuizDto>> GetAllQuiz()
        {
            var quiz = _mapper.Map<List<QuizDto>>(_quizRepository.GetAllQuiz());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(quiz);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(QuizDto))]
        [ProducesResponseType(400)]
        public ActionResult GetUser(int id)
        {
            if (!_quizRepository.QuizExist(id))
            {
                return NotFound();
            }
            var quiz = _mapper.Map<QuizDto>(_quizRepository.GetQuiz(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(quiz);

        }
        [HttpGet("with-questions-options/{QuizId}")]
        [ProducesResponseType(200, Type = typeof(QuizWithAnswersAndOptionsDto))]
        public ActionResult<QuizWithAnswersAndOptionsDto> GetQuizWithQuestionsAndOptions(int QuizId)
        {
            if (!_quizRepository.QuizExist(QuizId))
            {
                return NotFound();
            }

            var quiz = _mapper.Map<QuizWithAnswersAndOptionsDto>(_quizRepository.GetQuizWithQuestionsAndOptions(QuizId));
            if (quiz == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(quiz);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQuiz([FromBody] CreateQuizDto quiz)
        {
            if (quiz == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var QuizMap = _mapper.Map<Quiz>(quiz);
            if (!_quizRepository.CreateQuiz(QuizMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving ");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfuly");

        }

        [HttpPut("{QuizId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateQuiz([FromQuery] int QuizId,  [FromBody] CreateQuizDto quiz)
        {
            if (quiz == null)
            {
                return BadRequest(ModelState);
            }
            if (_quizRepository.QuizExist(QuizId))
            { return NotFound(); }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var QuizMap = _mapper.Map<Quiz>(quiz);
            QuizMap.Id = QuizId;
            if (!_quizRepository.UpdateQuiz(QuizMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

       
        [HttpDelete("{QuizId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteQuiz(int QuizId)
        {
            if (!_quizRepository.QuizExist(QuizId))
            {
                return NotFound();
            }
            if (!_quizRepository.DeleteQuiz(QuizId))
            {
                ModelState.AddModelError("", "Something Went Wrong While Removing");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}