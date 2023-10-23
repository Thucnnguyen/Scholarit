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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
	[Route("api")]
	[ApiController]
	public class QuizAttemptController : ControllerBase
	{
		private readonly IQuizAttemptQuestionService _quizAttemptQuestionService;
		private readonly IQuizAttemptService _quizAttemptService;
		private readonly IQuizService _quizService;
		private readonly IMapper _mapper;
		private readonly TokenUltil tokenUltil;

		public QuizAttemptController(IQuizAttemptQuestionService quizAttemptQuestionService, IQuizAttemptService quizAttemptService, IQuizService quizService, IMapper mapper)
		{
			_quizAttemptQuestionService = quizAttemptQuestionService;
			_quizAttemptService = quizAttemptService;
			_quizService = quizService;
			_mapper = mapper;
			tokenUltil = new TokenUltil();
		}

		// GET: api/<QuizAttemptController>
		[HttpGet("user/quizAttempt"), Authorize(Roles = "admin,user")]
		public async Task<ActionResult<QuizAttempDTO>> GetByUserIdAndQuizID([FromQuery] int QuizID)
		{
			var userId = int.Parse(tokenUltil.GetClaimByType(User, Enums.UserId).Value);
			var quizAttemp = await _quizAttemptService.GetQuizAttemptByUserIdAndQuizId(userId, QuizID);
			var QuizDTO = _mapper.Map<QuizAttempDTO>(quizAttemp);
			var quizAttempQuestionDTO = quizAttemp.QuizAttempQuestions.Select(qaq => _mapper.Map<QuizAttemptQuestionDTO>(qaq));
			quizAttemp.QuizAttempQuestions = quizAttempQuestionDTO.ToList();
			return Ok(QuizDTO);
		}

		[HttpGet("user/allQuizAttempt"), Authorize(Roles = "admin,user")]
		public async Task<ActionResult<PagingResultDTO<QuizAttempDTO>>> GetByUserId(	
			[FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
			[FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
			)
		{
			var userId = int.Parse(tokenUltil.GetClaimByType(User, Enums.UserId).Value);
			var quizAttemp = await _quizAttemptService.GetAllByUserId(pageNo, pageSize, true, userId);
			var quizDTO = quizAttemp.Items.Select( q => _mapper.Map<QuizAttempDTO>(q)).ToList();
			return Ok(new PagingResultDTO<QuizAttempDTO>()
			{
				Items = quizDTO,
				CurrentPage = pageNo,
				PageSize = pageSize,
				TotalItems = quizDTO.Count()
			});
		}

		[HttpGet("user/quiz/{quizId}/quizAttempt"), Authorize(Roles = "admin,user")]
		public async Task<ActionResult<QuizAttempDTO>> Get([FromRoute] int quizId)
		{
			var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;

			var quizAttemp = await _quizAttemptService.GetQuizAttemptByUserIdAndQuizId(int.Parse(userId), quizId);
			return Ok(quizAttemp);
		}

		// POST api/<QuizAttemptController>
		//[HttpPost]
		//public void Post([FromBody] string value)
		//{

		//}

		// PUT api/<QuizAttemptController>/5
		[HttpPost("quizAttempt"), Authorize(Roles = "admin,user")]
		public async Task<ActionResult<QuizAttempDTO>> Post([FromBody] QuizAttemptAddOrUpdateDTO quizAttemptAddOrUpdateDTO)
		{
			var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;
			var quizAttempDTO = await _quizAttemptService.AddQuizAttempt(quizAttemptAddOrUpdateDTO, int.Parse(userId));
			return Ok(quizAttempDTO);
		}

		// DELETE api/<QuizAttemptController>/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
