using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            if (existsName != null)
            {
                return BadRequest("That name role already exists");

            }
            // add new role
          
            var newId = await _service.AddRole(role);
            return Ok(newId);
          

        }

        [HttpDelete("{id}")]  // its mean update isdeleted = false
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            var existRole = await _service.GetRoleById(id);
            if (existRole == null) return NotFound("Not found user");

            var isdelete = await _service.DeleteRole(id);
            if (isdelete == false) return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Delete Failed. Error Server." });


            return Ok("Delete successful");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] RoleUpdateDTO roleUpdateDTO)
        {
            var existRole = await _service.GetRoleById(id);
            if (existRole is null) return NotFound();
            var roleUpdate = _mapper.Map<Role>(roleUpdateDTO);
           
            var existsName = await _service.IsExistNameRole(roleUpdateDTO.Name);
            if (existsName != null && existsName.Id != id)
            {
                return BadRequest("That name role already exists");

            }


            var result = await _service.UpdateRole(id, roleUpdate);
            if (result != null) return Ok(_mapper.Map<RoleDTO>(result));
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Update role failed. Server Error." });
        }
    }
}
