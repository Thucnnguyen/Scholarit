using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Entity;
using Scholarit.Service;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _service;
        private readonly IMapper _mapper;
        public ChapterController(IChapterService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/<ChapterController>
        [HttpGet("course/{courseId}/chapter")]
        public async Task<ActionResult<IEnumerable<ChapterDTO>>> GetAllChapterByCourseId([FromRoute] int courseId)
        {
            var chapterList = await _service.GetAllByCourseId(courseId, false);
            return Ok(chapterList.Select(c => _mapper.Map<ChapterDTO>(c)));
        }

        // GET api/<ChapterController>/5
        [HttpGet("chapter/{id}")]
        public async Task<ActionResult<Chapter>> GetByChapterId(int id)
        {
            var chapter = await _service.GetChapterByID(id);
            return Ok(_mapper.Map<ChapterDTO>(chapter));
        }

        // POST api/<ChapterController>
        [HttpPost("chapter")]
        public async Task<ActionResult<int>> Post([FromBody] ChapterAddDTO chapterAddDTO)
        {
            var chapter = _mapper.Map<Chapter>(chapterAddDTO);
            var newId = await _service.AddChapter(chapter);

            return Ok(newId);
        }

        // PUT api/<ChapterController>/5
        [HttpPut("chapter")]
        public async Task<ActionResult<ChapterDTO>> Put([FromBody] ChapterUpdateDTO chapterUpdateDTO)
        {
            var chapterUpdate = _mapper.Map<Chapter>(chapterUpdateDTO);
            var chapter = await _service.UpdateChapter(chapterUpdate);
            return Ok(_mapper.Map<ChapterDTO>(chapter));
        }

        // DELETE api/<ChapterController>/5
        [HttpDelete("chapter/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _service.DeleteChapterById(id);
            return Ok();
        }
    }
}
