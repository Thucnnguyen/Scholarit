using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IRoleService
    {
        Task<Role> GetRoleById(int id);
    }
}
