using AlumniProject.Dto;
using AlumniProject.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Entity;
using Scholarit.Service;
using Scholarit.Utils;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IRoleService roleService;
        private readonly TokenUltil tokenUltil;
        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration, IRoleService roleService)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
            this.roleService = roleService;
            tokenUltil = new TokenUltil();
        }


        // GET: api/<UserController>
        [HttpGet("user/info"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<UserDto>> Get()
        {
            var userId = int.Parse(tokenUltil.GetClaimByType(User, Enums.UserId).Value);
            var user = await _userService.GetUsersById(userId);
            return Ok(_mapper.Map<UserDto>(user));
        }

		[HttpGet("admin/user/{userId}/info"), Authorize(Roles = "admin")]
		public async Task<ActionResult<UserDto>> Get(
            [FromRoute] int userId
            )
		{
			var user = await _userService.GetUsersById(userId);
			return Ok(_mapper.Map<UserDto>(user));
		}

		[HttpGet("admin/user"), Authorize(Roles = "admin")]
        public async Task<ActionResult<UserDto>> GetAll(
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
            )
        {
            var user = await _userService.GetAllUsers(pageNo, pageSize);
            var userDTO = user.Items.Select(_ => _mapper.Map<UserDto>(_));
            return Ok(new PagingResultDTO<UserDto>()
            {
                CurrentPage = pageNo,
                PageSize = pageSize,
                TotalItems = userDTO.Count(),
                Items = userDTO.ToList()
            });
        }

        // GET api/<UserController>/5
        [HttpPost("/login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO login)
        {
            TokenHelper _tokenHelper = new TokenHelper(_configuration, roleService);
            var user = await _userService.GetUsers(login.Email, login.Password);
            var TokenString = await _tokenHelper.CreateToken(user);
            return Ok(TokenString);
        }

        // POST api/<UserController>
        [HttpPost("user")]
        public async Task<ActionResult<int>> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            var user = _mapper.Map<Users>(registerUserDTO);
            var newId = await _userService.AddUsers(user);
            return Ok(newId);
        }

        // PUT api/<UserController>/5
        [HttpPut("user"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UserUpdateDTO userUpdateDTO)
        {
            var userId = int.Parse(tokenUltil.GetClaimByType(User, Enums.UserId).Value);
            var user = _mapper.Map<Users>(userUpdateDTO);
            user.Id = userId;
            var updateUser = await _userService.UpdateUsers(user);
            return _mapper.Map<UserDto>(updateUser);
        }
        [HttpPost("user/sendingOTP")]

        public async Task<ActionResult> sendOTPToEmail([FromQuery] string email)
        {
            var user = await _userService.CreateOTP(email);
            EmailHelper.sendEmail(email, "OTP To reset password", "Your OTP: " + user.OTP);
            return Ok();
        }

        [HttpPost("user/checkingOTP")]
        public async Task<ActionResult> CheckOTP([FromBody] checkOTPDTO checkOTPDTO)
        {
            var check = await _userService.CheckOTP(checkOTPDTO);
            return Ok(check);
        }

        [HttpPut("user/changePassword")]
        public async Task<ActionResult<bool>> ChangePassword([FromBody]ChangePasswordDTO changePasswordDTO) 
        {
            _ = await _userService.ChangePassword(changePasswordDTO);
            return Ok(true);
        }
       
    }
}
