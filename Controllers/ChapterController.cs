using AlumniProject.Dto;
using AlumniProject.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Entity;
using Scholarit.Service;
using Scholarit.Utils;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _service;
        private readonly IEnrollService _enrollService;
        private readonly IMapper _mapper;
        private readonly TokenUltil tokenUltil;
        public ChapterController(IChapterService service, IMapper mapper, IEnrollService enrollService)
        {
            _service = service;
            _mapper = mapper;
            _enrollService = enrollService;
            tokenUltil = new TokenUltil();
        }

        // GET: api/<ChapterController>
        [HttpGet("user/course/{courseId}/chapter"), Authorize(Roles = "user")]
        public async Task<ActionResult<IEnumerable<ChapterDTO>>> GetAllChapterByCourseId([FromRoute] int courseId)
        {
            var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;
            var enroll = await _enrollService.GetEnrollByUserIdAndCourseId(int.Parse(userId), courseId);
            if (enroll == null)
            {
                return BadRequest("Not has permission");
            }
            var chapterList = await _service.GetAllByCourseId(courseId, false);
            return Ok(chapterList.Select(c => _mapper.Map<ChapterDTO>(c)));
        }
        [HttpGet("user/chapter/currentChapter/processing"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<CourseProcessingDTO>> GetcurrentCourseProcessing(
            )
        {
            var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;
            var processing = await _service.GetCurrentCourseProcessing(int.Parse(userId));
            return Ok(processing);
        }

        [HttpGet("admin/course/{courseId}/chapter"), Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<ChapterDTO>>> GetAllChapterByCourseIdforAdmin([FromRoute] int courseId)
        {
            var chapterList = await _service.GetAllByCourseId(courseId, false);
            return Ok(chapterList.Select(c => _mapper.Map<ChapterDTO>(c)));
        }

        // GET api/<ChapterController>/5
        [HttpGet("user/chapter/{id}"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<Chapter>> GetByChapterId(int id)
        {
            var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;

            var chapter = await _service.GetChapterByID(id);
            return Ok(_mapper.Map<ChapterDTO>(chapter));
        }

        // POST api/<ChapterController>
        [HttpPost("admin/chapter"), Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> Post([FromBody] ChapterAddDTO chapterAddDTO)
        {
            var chapter = _mapper.Map<Chapter>(chapterAddDTO);
            var newId = await _service.AddChapter(chapter);

            return Ok(newId);
        }

        // PUT api/<ChapterController>/5
        [HttpPut("admin/chapter"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ChapterDTO>> Put([FromBody] ChapterUpdateDTO chapterUpdateDTO)
        {
            var chapterUpdate = _mapper.Map<Chapter>(chapterUpdateDTO);
            var chapter = await _service.UpdateChapter(chapterUpdate);
            return Ok(_mapper.Map<ChapterDTO>(chapter));
        }

		[HttpPut("user/enroll/chapter"), Authorize(Roles = "admin,user")]
		public async Task<ActionResult<bool>> updateChapterEnroll([FromBody] UpdateEnrollDTO updateEnrollDTO)
		{
			var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;
			var check = await _service.UpdateChapterForEnroll(int.Parse(userId),updateEnrollDTO.CourseId,updateEnrollDTO.ChapterId);
			return Ok(_mapper.Map<ChapterDTO>(check));
		}

		// DELETE api/<ChapterController>/5
		[HttpDelete("admin/chapter/{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _service.DeleteChapterById(id);
            return Ok();
        }

    }
}
