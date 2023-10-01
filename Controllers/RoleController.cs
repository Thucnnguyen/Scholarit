using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Entity;
using Scholarit.Service;
using Scholarit.Service.ServiceImp;

namespace Scholarit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly IRoleService _service;
        private readonly IMapper _mapper;
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _service = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> Get()
        {
            var roles = await _service.GetRolesAsync();
            return Ok(roles.Select(_ => _mapper.Map<RoleDTO>(_)));
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] RoleAddDTO roleAddDTO)
        {
            var role = _mapper.Map<Role>(roleAddDTO);
            // check exists role name 
            var existsName = await _service.IsExistNameRole(roleAddDTO.Name);
            if (existsName == true)
            {
                return BadRequest("That name role already exists");

            }
            // add new role
          
            var newId = await _service.AddRole(role);
            return Ok(newId);
          

        }
    }
}
