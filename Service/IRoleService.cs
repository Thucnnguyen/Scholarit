using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IRoleService
    {
        Task<Role> GetRoleById(int id);

        Task<IEnumerable<Role>> GetRolesAsync();

        Task<bool> IsExistNameRole(string name);

        Task<int> AddRole(Role role);
    }
}
