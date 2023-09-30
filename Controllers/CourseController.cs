using AlumniProject.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Entity;
using Scholarit.Service;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _service = courseService;
            _mapper = mapper;
        }

        // GET: api/<CourseController>
        [HttpGet]
        public async Task<ActionResult<PagingResultDTO<CourseDTO>>> Get(
             [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
            )
        {
            var courseList = await _service.GetAllCourses(pageNo, pageSize);
            var courseListDTO = courseList.Items.Select(_ => _mapper.Map<CourseDTO>(_));
            return Ok(new PagingResultDTO<CourseDTO>()
            {
                CurrentPage = courseList.CurrentPage,
                PageSize = courseList.PageSize,
                Items = courseListDTO.ToList(),
                TotalItems = courseList.TotalItems
            });
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> Get(int id)
        {
            var courseDTO = await _service.GetCourseByID(id);

            return Ok(_mapper.Map<CourseDTO>(courseDTO));
        }

        // POST api/<CourseController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CourseAddDTO courseAddDTO)
        {
            var course = _mapper.Map<Course>(courseAddDTO);

            var newId = await _service.AddCourse(course);
            return Ok(newId);

        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseUpdateDTO>> Put([FromBody] CourseUpdateDTO courseUpdateDTO)
        {
            var course = _mapper.Map<Course>(courseUpdateDTO);
            var courseUpdate = await _service.UpdateCourse(course);
            return Ok(_mapper.Map<CourseDTO>(courseUpdate));
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var check = await _service.DeleteCourse(id);
            return Ok(check);
        }
    }
}
