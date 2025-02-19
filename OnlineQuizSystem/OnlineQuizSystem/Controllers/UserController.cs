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
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            this._userRepository = repository;
            this._mapper = mapper;
        }

        [HttpGet("Get-All")]
        [ProducesResponseType(200, Type = typeof(ICollection<UserDto>))]
        public ActionResult<ICollection<User>> GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        public ActionResult GetUser(int id)
        {
            if (!_userRepository.UserExist(id))
            {
                return NotFound();
            }
            var user= _mapper.Map<UserDto>(_userRepository.GetUser(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);

        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] CreateUserDto user)
        {
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserMap = _mapper.Map<User>(user);
            if (!_userRepository.CreateUser(UserMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving ");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfuly");

        }

        [HttpPut("{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateUser([FromBody] UpdateUserDto user)
        {
            if (user == null)
            {
                return BadRequest(ModelState);
            }
            if (_userRepository.UserExist(user.Id))
            { return NotFound(); }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var UserMap = _mapper.Map<User>(user);
            if (!_userRepository.UpdateUser(UserMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{UserId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int UserId)
        {
            if (!_userRepository.UserExist(UserId))
            {
                return NotFound();
            }
            if (!_userRepository.DeleteUser(UserId))
            {
                ModelState.AddModelError("", "Something Went Wrong While Removing");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}