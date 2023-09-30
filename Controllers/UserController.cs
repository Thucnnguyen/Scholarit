using AlumniProject.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Entity;
using Scholarit.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IRoleService roleService;
        public UserController(IUserService userService,IMapper mapper, IConfiguration configuration, IRoleService roleService)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
            this.roleService = roleService;
        }


        // GET: api/<UserController>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _userService.GetUsersById(id);
            return Ok(_mapper.Map<UserDto>(user));
        }

        // GET api/<UserController>/5
        [HttpPost("/login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO login)
        {
            TokenHelper _tokenHelper = new TokenHelper(_configuration,roleService);
            var user = await _userService.GetUsers(login.Email,login.Password);
            var TokenString = await _tokenHelper.CreateToken(user);
            return Ok(TokenString);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<int>> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            var user = _mapper.Map<Users>(registerUserDTO);
            var newId = await _userService.AddUsers(user);
            return Ok(newId);
        }

        // PUT api/<UserController>/5
        [HttpPut()]
        public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UserUpdateDTO userUpdateDTO)
        {
            var user = _mapper.Map<Users>(userUpdateDTO);
            var updateUser = await _userService.UpdateUsers(user);    
            return _mapper.Map<UserDto>(updateUser);
        }

        // DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}


        [HttpGet]    // get all user where isdeleted = false
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();
            return Ok(_mapper.Map<List<UserDto>>(result));
        }
    }
}
