using AlumniProject.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly IQuizQuestionService _quizQuestionService;
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuizController(IQuizService quizService, IQuizQuestionService quizQuestionService, IQuestionService questionService, IMapper mapper)
        {
            _quizService = quizService;
            _quizQuestionService = quizQuestionService;
            _questionService = questionService;
            _mapper = mapper;
        }

        // GET: api/<QuizController>
        [HttpGet("user/quiz/{id}"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<QuizForShowQuestionDTO>> GetById(int id)
        {
            var quiz = await _quizService.GetQuizByID(id);
            List<QuestionDTO> questionList = new List<QuestionDTO>();
            var quizquestion = quiz.QuizQuestions.OrderBy(qq => qq.Order);
            foreach (QuizQuestion qq in quizquestion)
            {
                var question = await _questionService.GetQuestionByID(qq.QuestionId);
                questionList.Add(_mapper.Map<QuestionDTO>(question));
            }
            return Ok(new QuizForShowQuestionDTO()
            {
                ChapterId = quiz.ChapterId,
                Duration = quiz.Duration,
                MaxScore = quiz.MaxScore,
                NumberOfQuestion = quiz.MaxScore,
                Name = quiz.Name,
                Questions = questionList,
            });
        }

        // GET api/<QuizController>/5
        [HttpGet("user/chapter/{chapterId}/quiz"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<PagingResultDTO<QuizForShowQuestionDTO>>> Get(
            [FromRoute] int chapterId,
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
            )
        {

            var quiz = await _quizService.GetAllByChapterId(pageNo, pageSize, true, chapterId);
            var quizDTO = quiz.Items.Select(q => _mapper.Map<QuizDTO>(q)).ToList();
            return Ok(new PagingResultDTO<QuizDTO>()
            {
                CurrentPage = pageNo,
                Items = quizDTO,
                PageSize = pageSize,
                TotalItems = quiz.TotalItems,
            });
        }

        // POST api/<QuizController>
        [HttpPost("admin/quiz"), Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> Post([FromBody] QuizAddDTO quizAddDTO)
        {
            var quiz = new Quiz()
            {
                ChapterId = quizAddDTO.ChapterId,
                Duration = quizAddDTO.Duration,
                MaxScore = quizAddDTO.QuestionIdList.Count()*10,
                NumberOfQuestion = quizAddDTO.QuestionIdList.Count(),
                Name = quizAddDTO.Name,
            };
            var newQuizId = await _quizService.AddQuiz(quiz);
            int order = 0;
            foreach(int questionID in quizAddDTO.QuestionIdList)
            {

                var newQuizQuestion = new QuizQuestion()
                {
                    QuestionId = questionID,
                    QuizId = newQuizId,
                    Order = order,
                };
                await _quizQuestionService.AddQuizQuestion(newQuizQuestion);
                order++;
            }
            return newQuizId;
        }

        // PUT api/<QuizController>/5
        [HttpPut("admin/quiz"), Authorize(Roles = "admin")]
        public async Task<ActionResult> Put([FromBody] QuizQuestionUpdateDTO quizQuestionUpdateDTO)
        {
            await _quizQuestionService.DeleteQuizQuestionByQuizId(quizQuestionUpdateDTO.ChapterId);

            var quiz = new Quiz()
            {
                ChapterId = quizQuestionUpdateDTO.ChapterId,
                Duration = quizQuestionUpdateDTO.Duration,
                MaxScore = quizQuestionUpdateDTO.QuestionIdList.Count(),
                NumberOfQuestion = quizQuestionUpdateDTO.QuestionIdList.Count(),
                Name = quizQuestionUpdateDTO.Name,
            };
            var newQuizId = await _quizService.AddQuiz(quiz);
            int order = 0;
            foreach (int questionID in quizQuestionUpdateDTO.QuestionIdList)
            {

                var newQuizQuestion = new QuizQuestion()
                {
                    QuestionId = questionID,
                    QuizId = newQuizId,
                    Order = order,
                };
                await _quizQuestionService.AddQuizQuestion(newQuizQuestion);
                order++;
            }
            return Ok();
        }

        // DELETE api/<QuizController>/5
        [HttpDelete("admin/quiz/{id}"), Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _quizQuestionService.DeleteQuizQuestionById(id);
            return Ok();
        }
    }
}
