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
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
	[Route("api")]
	[ApiController]
	public class CourseController : ControllerBase
	{
		private readonly ICourseService _service;
		private readonly IMapper _mapper;
		private readonly TokenUltil tokenUltil;

		public CourseController(ICourseService courseService, IMapper mapper)
		{
			_service = courseService;
			_mapper = mapper;
			tokenUltil = new TokenUltil();
		}

		// GET: api/<CourseController>
		//[HttpGet("user/course"), Authorize(Roles = "admin,user")]
		[HttpGet("user/course")]
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



		//[HttpGet("user/category/{categoryId}/course"), Authorize(Roles = "admin,user")]
		[HttpGet("user/category/{categoryId}/course")]
		public async Task<ActionResult<PagingResultDTO<CourseDTO>>> Get(
			[FromRoute] int categoryId,
			 [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
			[FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
			)
		{
			var courseList = await _service.GetAllCoursesByCategoriesId(pageNo, pageSize, categoryId);
			var courseListDTO = courseList.Items.Select(_ => _mapper.Map<CourseDTO>(_));
			return Ok(new PagingResultDTO<CourseDTO>()
			{
				CurrentPage = courseList.CurrentPage,
				PageSize = courseList.PageSize,
				Items = courseListDTO.ToList(),
				TotalItems = courseList.TotalItems
			});
		}

		[HttpGet("user/courseWithMostView")]
		public async Task<ActionResult<PagingResultDTO<CourseDTO>>> GetCourseWithMostView(
			 [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
			[FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
			)
		{
			var courseList = await _service.GetAllCourseWithMostView(pageNo, pageSize);
			var courseListDTO = courseList.Items.Select(_ => _mapper.Map<CourseDTO>(_));
			return Ok(new PagingResultDTO<CourseDTO>()
			{
				CurrentPage = courseList.CurrentPage,
				PageSize = courseList.PageSize,
				Items = courseListDTO.ToList(),
				TotalItems = courseList.TotalItems
			});
		}

		[HttpGet("user/course/finished"), Authorize(Roles = "admin,user")]
		public async Task<ActionResult<PagingResultDTO<CourseDTO>>> GetFinishedCourse(
			[FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
			[FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
			)
		{
			var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;
			var courseList = await _service.GetAllFinishedCourses(pageNo, pageSize, int.Parse(userId));
			var courseListDTO = courseList.Items.Select(_ => _mapper.Map<CourseDTO>(_));
			return Ok(new PagingResultDTO<CourseDTO>()
			{
				CurrentPage = courseList.CurrentPage,
				PageSize = courseList.PageSize,
				Items = courseListDTO.ToList(),
				TotalItems = courseList.TotalItems
			});
		}

		[HttpGet("user/enrollCourse"), Authorize(Roles = "admin,user")]
		public async Task<ActionResult<PagingResultDTO<CourseDTO>>> GetUserEnrollCourse(
			[FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
			[FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
			)
		{
			var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;
			var courseList = await _service.GetAllCoursesByUserId(pageNo, pageSize, int.Parse(userId));
			var courseListDTO = courseList.Items.Select(_ => _mapper.Map<CourseDTO>(_));
			return Ok(new PagingResultDTO<CourseDTO>()
			{
				CurrentPage = courseList.CurrentPage,
				PageSize = courseList.PageSize,
				Items = courseListDTO.ToList(),
				TotalItems = courseList.TotalItems
			});
		}

		[HttpGet("user/course/searching/{searchString}")]
		public async Task<ActionResult<PagingResultDTO<CourseDTO>>> Get(
			[FromRoute] string searchString,
			 [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
			[FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
			)
		{
			var courseList = await _service.SearchCourseByName(pageNo, pageSize, searchString);
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
		[HttpGet("user/relatedCourse"), Authorize(Roles = "admin,user")]
		public async Task<ActionResult<PagingResultDTO<CourseDTO>>> GetRelatedCourse(
			 [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
			[FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
			)
		{
			var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;
			var courseList = await _service.GetAllRelatedCourse(pageNo, pageSize, int.Parse(userId));
			var courseListDTO = courseList.Items.Select(_ => _mapper.Map<CourseDTO>(_));
			return Ok(new PagingResultDTO<CourseDTO>()
			{
				CurrentPage = courseList.CurrentPage,
				PageSize = courseList.PageSize,
				Items = courseListDTO.ToList(),
				TotalItems = courseList.TotalItems
			});
		}

		[HttpGet("user/course/{id}")]
		public async Task<ActionResult<CourseDTO>> Get([FromRoute] int id)
		{
			var courseDTO = await _service.GetCourseByID(id);

			return Ok(_mapper.Map<CourseDTO>(courseDTO));
		}

		// POST api/<CourseController>
		[HttpPost("admin/course"), Authorize(Roles = "admin")]
		public async Task<ActionResult<int>> Post([FromBody] CourseAddDTO courseAddDTO)
		{
			var course = _mapper.Map<Course>(courseAddDTO);

			var newId = await _service.AddCourse(course);
			return Ok(newId);

		}

		// PUT api/<CourseController>/5
		[HttpPut("admin/course"), Authorize(Roles = "admin")]
		public async Task<ActionResult<CourseUpdateDTO>> Put([FromBody] CourseUpdateDTO courseUpdateDTO)
		{
			var course = _mapper.Map<Course>(courseUpdateDTO);
			var courseUpdate = await _service.UpdateCourse(course);
			return Ok(_mapper.Map<CourseDTO>(courseUpdate));
		}

		[HttpPut("user/course/note"), Authorize(Roles = "user")]
		public async Task<ActionResult<bool>> updateNote([FromBody] UpdateNoteForCourse updateNoteForCourse)
		{
			var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;
			var courseUpdate = await _service.UpdateNote(updateNoteForCourse.CourseId, int.Parse(userId), updateNoteForCourse.Note);
			return Ok(courseUpdate);
		}

		// DELETE api/<CourseController>/5
		[HttpDelete("admin/course/{id}"), Authorize(Roles = "admin")]
		public async Task<ActionResult<bool>> Delete(int id)
		{
			var check = await _service.DeleteCourse(id);
			return Ok(check);
		}
	}
}
