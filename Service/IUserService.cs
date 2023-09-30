using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IUserService
    {
        Task<Users> GetUsers(string email, string password);
        Task<Users> GetUsersByEmail(string email);
        Task<Users> GetUsersById(int id);
        Task<Users> UpdateUsers(Users users);
        Task<bool> DeleteUsers(int id);
        Task<int> AddUsers(Users users);

    }
}
