using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;
using Scholarit.Utils;

namespace Scholarit.Service.ServiceImp
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly PasswordHelper _passwordHelper;
        public UserService(IUserRepo repo)
        {
            _repo = repo;
            _passwordHelper = new PasswordHelper();
        }

        public async Task<int> AddUsers(Users users)
        {
            var User = await _repo.FindOneByCondition(u => u.Email == users.Email && u.IsDeleted == false);
            if (User != null)
            {
                throw new ConflictException("Users was existed with email: "+ users.Email);
            }

            string pass = _passwordHelper.HashPassword(users.Password);
            users.Password = pass;

            var idNew = await _repo.CreateAsync(users);
            return idNew;
        }

        public async Task<bool> DeleteUsers(int id)
        {
            var user = await _repo.FindOneByCondition(u => u.Id == id && u.IsDeleted == false);

            if(user == null)
            {
                throw new NotFoundException("Users not found with id: " + id);
            }

            user.IsDeleted = true;
            await UpdateUsers(user);
            return true;

        }

        public async Task<Users> GetUsers(string email, string password)
        {
            var passwordHash = _passwordHelper.HashPassword(password);
            var user = await _repo.FindOneByCondition(
                u => u.Email == email && 
                u.IsDeleted == false &&
                u.Password == passwordHash);

            if(user == null)
            {
                throw new NotFoundException("Email or password is wrong!");
            }

            return user;
        }

        public async Task<Users> GetUsersByEmail(string email)
        {
            var user = await _repo.FindOneByCondition(
                u => u.Email == email &&
                u.IsDeleted == false );

            if (user == null)
            {
                throw new NotFoundException("Email is wrong!");
            }

            return user;
        }

        public async Task<Users> UpdateUsers(Users users)
        {
                // check if the user exists in the database
                var existingUser = await _repo.FindOneByCondition(u => u.Id == users.Id && u.IsDeleted == false);
                if (existingUser == null)
                {
                    throw new NotFoundException("User not found with id: " + users.Id);
                }

                // update the user properties with the new values
                existingUser.FullName = users.FullName;
                existingUser.Dob = users.Dob;
                existingUser.Address = users.Address;
                existingUser.Hobby = users.Hobby;
                existingUser.LastLogin = users.LastLogin;
                existingUser.LearnHourPerDay = users.LearnHourPerDay;
                existingUser.Strength = users.Strength;
                existingUser.Email = users.Email;
                existingUser.Password = users.Password;
                existingUser.AvatarUrl = users.AvatarUrl;
                existingUser.RoleId = users.RoleId;

                // save the changes to the database
                await _repo.UpdateAsync(existingUser);

                // return the updated user
                return existingUser;

        }
    }
}
