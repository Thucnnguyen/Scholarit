using Scholarit.Data.Repository;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }

        public async Task<int> AddUsers(Users users)
        {
            // var User = await _repo.FindOneByCondition(u => u.Email == users.Email);
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUsers(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetUsers(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetUsersByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Users> UpdateUsers(Users users)
        {
            throw new NotImplementedException();
        }
    }
}
