using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OnlineQuizSystem.Data;
using OnlineQuizSystem.Dto_s;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;
using OnlineQuizSystem.Repositories;

namespace OnlineQuizSystem.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserAnswerController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;
        private readonly IUserAnswerRepository _answerRepository;
        private readonly IQuizQuestionRepository _questionRepository;
        private readonly IQuestionOptionRepository _optionRepository;
        private readonly ISessionRepository _sessionRepository;

        public UserAnswerController(IDistributedCache cache, IMapper mapper, IUserAnswerRepository answerRepository, IQuizQuestionRepository questionRepository, IQuestionOptionRepository optionRepository, ISessionRepository sessionRepository)
        {
            this._cache = cache;
            this._mapper = mapper;
            this._answerRepository = answerRepository;
            this._questionRepository = questionRepository;
            this._optionRepository = optionRepository;
            this._sessionRepository= sessionRepository;
        }

        [HttpGet("Get-All")]
        [ProducesResponseType(200, Type = typeof(ICollection<UserAnswerDto>))]
        public ActionResult<ICollection<User>> GetAnswers()
        {
            var answers = _mapper.Map<List<UserAnswerDto>>(_answerRepository.GetAllAnswers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(answers);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserAnswerDto))]
        [ProducesResponseType(400)]
        public ActionResult GetAnswer(int id)
        {
            if (!_answerRepository.AnswerExist(id))
            {
                return NotFound();
            }
            var answer = _mapper.Map<UserAnswerDto>(_answerRepository.GetAnswer(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(answer);

        }

        [HttpPost("Add-answers/{SessionId}")]
        public IActionResult AddAnswerToSession (CreateUserAnswerDto answerDto, int SessionId)
        {
            if (!_sessionRepository.SessionExist(SessionId))
            {
                return NotFound();
            }
            if (_sessionRepository.isSessionEnd(SessionId))
            {
                return BadRequest("Session Tamamlanmış");
            }
            if (!_questionRepository.QuestionExist(answerDto.QuestionId) || !_optionRepository.OptionExist(answerDto.OptionId))
            {
                return NotFound();
            }
            var cachedAnswers = _cache.GetString($"session:{SessionId}:answers");
            var answers=string.IsNullOrEmpty(cachedAnswers) 
                ? new List<CreateUserAnswerDto>()
                : JsonConvert.DeserializeObject<List<CreateUserAnswerDto>>(cachedAnswers);
            answers.Add(answerDto);

            _cache.SetString($"session:{SessionId}:answers", JsonConvert.SerializeObject(answers), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            });
            return Ok("Cevap eklendi");
        }

        [HttpPut("{AnswerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateAnswer([FromBody] UpdateAnswerDto answer, int OptionId)
        {
            if (answer == null)
            {
                return BadRequest(ModelState);
            }
            if (_answerRepository.AnswerExist(answer.Id))
            { return NotFound(); }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var option = _optionRepository.GetOption(OptionId);
            answer.QuestionOption= option;
            var AnswerMap = _mapper.Map<QuizUserAnswer>(answer);
            
            if (!_answerRepository.UpdateAnswer(AnswerMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{AnswerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAnswer(int AnswerId)
        {
            if (!_answerRepository.AnswerExist(AnswerId))
            {
                return NotFound();
            }
            if (!_answerRepository.DeleteAnswer(AnswerId))
            {
                ModelState.AddModelError("", "Something Went Wrong While Removing");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        } 

    }
}
