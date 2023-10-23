using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Entity;
using Scholarit.Service;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
    [Route("api")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService category, IMapper mapper)
        {
            _service = category;
            _mapper = mapper;
        }

        // GET: api/<CategoryController>
        [HttpGet("user/category")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _service.GetAll(false);
            return Ok(categories.Select(_ => _mapper.Map<CategoryDTO>(_)));
        }

        // GET api/<CategoryController>/5
        [HttpGet("user/category/{id}")]
        public async Task<ActionResult<CategoryDTO>> Get([FromRoute] int id)
        {
            var categories = await _service.GetCategoryByID(id);
            return Ok(_mapper.Map<CategoryDTO>(categories));
        }

        // POST api/<CategoryController>
        [HttpPost("admin/category"), Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> Post([FromBody] CategoryAddDTO categoryAddDTO)
        {
            var category = _mapper.Map<Category>(categoryAddDTO);
            return Ok(await _service.AddCategory(category));
        }

        // PUT api/<CategoryController>/5
        [HttpPut("admin/category"), Authorize(Roles = "admin")]
        public async Task<ActionResult<Category>> Put( [FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {
            var categoryUpdate = await _service.UpdateCategory(_mapper.Map<Category>(categoryUpdateDTO));
            return Ok(categoryUpdate);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("admin/category/{id}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var check = await _service.DeleteCategory(id);
            return Ok(check);
        }
    }
}
