using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystem.Dto_s;
using OnlineQuizSystem.Interfaces;
using OnlineQuizSystem.Models;
using OnlineQuizSystem.Repositories;

namespace OnlineQuizSystem.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OptionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IQuizQuestionRepository _questionRepository;
        private readonly IQuestionOptionRepository _optionRepository;

        public OptionController(IMapper mapper, IQuizQuestionRepository _questionRepository, IQuestionOptionRepository optionRepository)
        {
            this._mapper = mapper;
            this._questionRepository = _questionRepository;
            this._optionRepository = optionRepository;
        }

        [HttpGet("Get-All")]
        [ProducesResponseType(200, Type = typeof(ICollection<OptionDto>))]
        public ActionResult<ICollection<OptionDto>> GetAllOptions()
        {
            var options = _mapper.Map<List<OptionDto>>(_optionRepository.GetAllOptions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(options);
        }

        [HttpGet("by-question/{QuestionId}")]
        [ProducesResponseType (200, Type= typeof(ICollection<OptionDto>))]

        public ActionResult<ICollection<OptionDto>> GetOptionsOfAQuestion (int QuestionId)
        {
            
            if (!_questionRepository.QuestionExist(QuestionId))
            {
                return NotFound();
            }
            var options = _mapper.Map<ICollection<OptionDto>>(_optionRepository.GetOptionsOfAQuestion(QuestionId));
            if (options == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(options);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(OptionDto))]
        [ProducesResponseType(400)]
        public ActionResult GetOption(int id)
        {
            if (!_optionRepository.OptionExist(id))
            {
                return NotFound();
            }
            var option = _mapper.Map<OptionDto>(_optionRepository.GetOption(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(option);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOption([FromQuery] int QuestionId, [FromBody] CreateOptionDto option)
        {
            if (option == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (QuestionId == null)
            {
                return BadRequest();
            }
            if (!_questionRepository.QuestionExist(QuestionId))
            {
                return NotFound();
            }

            var OptionMap = _mapper.Map<QuestionOption>(option);
            OptionMap.QuizQuestion= _questionRepository.GetQuestion(QuestionId);
            if (!_optionRepository.CreateOption(OptionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving ");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfuly");

        }

        [HttpPut("{OptionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateOption([FromQuery] int OptionId, [FromBody] CreateOptionDto option)
        {
            if (option == null)
            {
                return BadRequest(ModelState);
            }
            if (_optionRepository.OptionExist(OptionId))
            { return NotFound(); }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var OptionMap = _mapper.Map<QuestionOption>(option);
            OptionMap.Id = OptionId;
            if (!_optionRepository.UpdateOption(OptionMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{OptionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOption(int OptionId)
        {
            if (!_optionRepository.OptionExist(OptionId))
            {
                return NotFound();
            }
            if (!_optionRepository.DeleteOption(OptionId))
            {
                ModelState.AddModelError("", "Something Went Wrong While Removing");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
