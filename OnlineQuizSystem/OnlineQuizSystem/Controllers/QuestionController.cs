using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Dto_s;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;
using System.Collections.Generic;

namespace OnlineQuizSystem.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;
        private readonly IQuizQuestionRepository _questionRepository;

        public QuestionController(IQuizRepository repository, IMapper mapper, IQuizQuestionRepository questionRepository)
        {
            this._quizRepository = repository;
            this._mapper = mapper;
            this._questionRepository = questionRepository;
        }

        [HttpGet("Get-All")]
        [ProducesResponseType(200, Type = typeof(ICollection<QuestionDto>))]
        public ActionResult<ICollection<QuestionDto>> GetAllQuestions()
        {
            var questions = _mapper.Map<List<QuestionDto>>(_questionRepository.GetAllQuestions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(questions);

        }

        [HttpGet("by-quiz/{QuizId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<QuestionDto>))]
        public ActionResult<QuestionDto> GetQuestionsOfAQuiz(int QuizId)
        {
            if (!_quizRepository.QuizExist(QuizId))
            {
                return NotFound();
            }
            var questions = _mapper.Map<ICollection<QuestionDto>>(_questionRepository.GetQuestionsOfAQuiz(QuizId));
            if (questions == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(questions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(QuestionDto))]
        [ProducesResponseType(400)]
        public ActionResult GetQuestions(int id)
        {
            if (!_questionRepository.QuestionExist(id))
            {
                return NotFound();
            }
            var question = _mapper.Map<QuestionDto>(_questionRepository.GetQuestion(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(question);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateQuestion([FromQuery] int QuizId , [FromBody] CreateQuestionDto question)
        {
            if (question == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (QuizId == null)
            {
                return BadRequest();
            }
            if (!_quizRepository.QuizExist(QuizId))
            {
                return NotFound();
            }

            var QuestionMap = _mapper.Map<QuizQuestion>(question);
            QuestionMap.Quiz= _quizRepository.GetQuiz(QuizId);
            if (!_questionRepository.CreateQuestion(QuestionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving ");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfuly");

        }

        [HttpPut("{QuestionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateQuestion([FromQuery] int QuestionId, [FromBody] CreateQuestionDto question)
        {
            if (question == null)
            {
                return BadRequest(ModelState);
            }
            if (_questionRepository.QuestionExist(QuestionId))
            { return NotFound(); }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var QuestionMap = _mapper.Map<QuizQuestion>(question);
            QuestionMap.Id = QuestionId;
            if (!_questionRepository.UpdateQuestion(QuestionMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{QuestionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteQuestion(int QuestionId)
        {
            if (!_questionRepository.QuestionExist(QuestionId))
            {
                return NotFound();
            }
            if (!_questionRepository.DeleteQuestion(QuestionId))
            {
                ModelState.AddModelError("", "Something Went Wrong While Removing");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}