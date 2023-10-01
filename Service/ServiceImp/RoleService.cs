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

        public async Task<int> AddRole(Role role)
        {
            var newRoleId = await _roleRepo.CreateAsync(role);
            return newRoleId;
        }

        public async Task<bool> DeleteRole(int id)
        {
            var role = await _roleRepo.FindOneByCondition(u => u.Id == id && u.IsDeleted == false);

            if (role == null)
            {
                throw new NotFoundException("Roles not found with id: " + id);
            }

            role.IsDeleted = true;
            await _roleRepo.UpdateAsync(role);
            return true;
        }

        public async Task<Role?> GetRoleById(int id)
        {
            var role = await _roleRepo.FindOneByCondition(r => r.Id == id);
            return role != null ? role : throw new NotFoundException("Role not found with id: " + id) ;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            var roles = await _roleRepo.GetAllAsync();

            return roles;
        }

        public async Task<bool> IsExistNameRole(string name)
        {
            var role = await _roleRepo.FindOneByCondition(r => r.Name.Equals(name) && r.IsDeleted == false);    
            if(role is null) return false;
            return role != null;    
        }
    }
}
