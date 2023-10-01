using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IRoleService
    {
        Task<Role?> GetRoleById(int id);

        Task<IEnumerable<Role>> GetRolesAsync();

        Task<Role?> IsExistNameRole(string name);
        

        Task<int> AddRole(Role role);
        Task<bool> DeleteRole(int id);
        Task<Role> UpdateRole(int id, Role role);
    }
}
