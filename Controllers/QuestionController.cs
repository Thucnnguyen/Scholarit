using AlumniProject.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Scholarit.DTO;
using Scholarit.Entity;
using Scholarit.Service;
using System.ComponentModel.DataAnnotations;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
    [Route("api")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _service;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/<ValuesController>
        [HttpGet("user/chapter/{chapterId}/question"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<PagingResultDTO<QuestionDTO>>> GetByChapterID(
            [FromRoute] int chapterId,
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
            )
        {
            var questionPage = await _service.GetQuestionByChapterId(pageNo, pageSize, chapterId);
            var questionDTOList = questionPage.Items.Select(q => _mapper.Map<QuestionDTO>(q)).ToList();
            return Ok(new PagingResultDTO<QuestionDTO>()
            {
                CurrentPage = questionPage.CurrentPage,
                PageSize = questionPage.PageSize,
                Items = questionDTOList,
                TotalItems = questionPage.TotalItems
            });
        }
        [HttpGet("user/chapter/{chapterId}/question/random"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<QuestionDTO>> GetByChapterIDrandom(
            [FromRoute] int chapterId
            )
        {
            var questionRandom = await _service.GetQuestionByChapterIdRandom(chapterId);
            return Ok(_mapper.Map<QuestionDTO>(questionRandom));
        }

        [HttpGet("user/chapter/{chapterId}/question/random/checking"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<QuestionAnwserCheckDTO>> GetByChapterIDrandomChecking(
            [FromBody] QuestionAnwserDTO questionAnwserDTO
            )
        {
            var questionRandom = await _service.GetQuestionByChapterIdRandomCheck(questionAnwserDTO);
            return Ok(questionRandom);
        }

        [HttpGet("admin/chapter/{chapterId}/question"), Authorize(Roles = "admin")]
        public async Task<ActionResult<PagingResultDTO<QuestionDTO>>> GetByChapterIDForAdmin(
            [FromRoute] int chapterId,
             [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
            )
        {
            var questionPage = await _service.GetQuestionByChapterId(pageNo, pageSize, chapterId);
            var questionDTOList = questionPage.Items.Select(q => _mapper.Map<QuestionForAdminDTO>(q)).ToList();
            return Ok(new PagingResultDTO<QuestionForAdminDTO>()
            {
                CurrentPage = questionPage.CurrentPage,
                PageSize = questionPage.PageSize,
                Items = questionDTOList,
                TotalItems = questionPage.TotalItems
            });
        }
        // GET api/<ValuesController>/5
        [HttpGet("user/question/{id}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<QuestionDTO>> Get(int id)
        {
            var question = await _service.GetQuestionByID(id);
            return Ok(_mapper.Map<QuestionDTO>(question));
        }

        // POST api/<ValuesController>
        [HttpPost("admin/question"), Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> Post([FromBody] QuestionAddDTO questionAddDTO)
        {
            var question = _mapper.Map<Question>(questionAddDTO);
            var newQuestionId = await _service.AddQuestion(question);
            return Ok(newQuestionId);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("admin/question"), Authorize(Roles = "admin")]
        public async Task<ActionResult<QuestionUpdateDTO>> Put( [FromBody] QuestionUpdateDTO questionUpdateDTO)
        {
            var question = _mapper.Map<Question>(questionUpdateDTO);
            var questionUpdate = await _service.UpdateQuestion(question);
            return Ok(_mapper.Map<QuestionForAdminDTO>(questionUpdate));
        }

		[HttpPost("admin/questionlist"), Authorize(Roles = "admin")]
		public async Task<ActionResult<List<int>>> PostList([FromBody] List<QuestionAddDTO> questionUpdateDTO)
		{
            var newQuestionIdList = new List<int>();
            foreach(var q in questionUpdateDTO)
            {
				var question = _mapper.Map<Question>(q);
				var questionId = await _service.AddQuestion(question);
				newQuestionIdList.Add(questionId);
			}
			
			return Ok(newQuestionIdList);
		}

		// DELETE api/<ValuesController>/5
		[HttpDelete("admin/question/{id}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var check = await _service.DeleteQuestionById(id);
            return Ok(check);
        }
    }
}
