using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Entity;
using Scholarit.Service;
using System.Data;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
    [Route("api")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _service;
        private readonly IMapper _mapper;
        public ResourceController(IResourceService resourceService, IMapper mapper)
        {
            _service = resourceService;
            _mapper = mapper;
        }

        // GET: api/<ResourceController>
        [HttpGet("user/chapter/{chapterId}/resource"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<IEnumerable<ResourceDTO>>> GetByChapterId([FromRoute]int chapterId)
        {
            var resourceList = await _service.GetByChapterId(chapterId);
            return Ok(resourceList.Select(r => _mapper.Map<ResourceDTO>(r)));
        }

        // GET api/<ResourceController>/5
        [HttpGet("user/resource/{id}"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<ResourceDTO>> Get(int id)
        {
            var ExisitingResource = await _service.GetById(id);
            
            return Ok(_mapper.Map<ResourceDTO>(ExisitingResource));
        }

        // POST api/<ResourceController>
        [HttpPost("admin/resource"), Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> Post([FromBody] ResourceAddDTO resourceAddDTO)
        {
            var newId = await _service.AddResource(_mapper.Map<Resource>(resourceAddDTO));
            return newId;
        }

        // PUT api/<ResourceController>/5
        [HttpPut("admin/resource"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ResourceDTO>> Put([FromBody] ResourceUpdateDTO resourceUpdateDTO)
        {
            var resouce = await _service.UpdateResource(_mapper.Map<Resource>(resourceUpdateDTO));
            return Ok(_mapper.Map<ResourceDTO>(resouce));
        }

        // DELETE api/<ResourceController>/5
        [HttpDelete("admin/resource/{id}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var resource = await _service.DeleteResource(id);
            return Ok(resource);
        }
    }
}
