using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepo _roleRepo;
        public RoleService(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await _roleRepo.FindOneByCondition(r => r.Id == id);
            return role != null ? role : throw new NotFoundException("Role not found with id: " + id) ;
        }
    }
}
