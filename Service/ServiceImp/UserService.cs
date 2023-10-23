using AlumniProject.Dto;
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
                throw new ConflictException("Users was existed with email: " + users.Email);
            }

            string pass = _passwordHelper.HashPassword(users.Password);
            users.Password = pass;
            users.DateCreated = DateTime.Now;
            var idNew = await _repo.CreateAsync(users);
            return idNew;
        }

        public async Task<bool> DeleteUsers(int id)
        {
            var user = await _repo.FindOneByCondition(u => u.Id == id && u.IsDeleted == false);

            if (user == null)
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
                _ => _.Email == email &&
                _.IsDeleted == false,
                _ => _.Role);

            if (user == null)
            {
                throw new NotFoundException("Email or password is wrong!");
            }

            if (!_passwordHelper.CheckHashPwd(password, user.Password))
            {
                throw new NotFoundException("Email or password is wrong!");
            }
            user.LastLogin = DateTime.Now;

            await _repo.UpdateAsync(user);

            return user;
        }

        public async Task<PagingResultDTO<Users>> GetAllUsers(int pageNo, int pageSize)
        {

            var user = await _repo.GetAllByConditionAsync(pageNo, pageSize, _ => _.IsDeleted != true, _ => _.DateCreated, true);

            if (user == null)
            {
                throw new NotFoundException("Empty users!");
            }

            return user;
        }

        public async Task<Users> GetUsersByEmail(string email)
        {
            var user = await _repo.FindOneByCondition(
                u => u.Email == email &&
                u.IsDeleted == false,
                _ => _.Role);

            if (user == null)
            {
                throw new NotFoundException("Email is wrong!");
            }

            return user;
        }

        public async Task<Users> GetUsersById(int id)
        {
            var existingUser = await _repo.FindOneByCondition(u => u.Id == id && u.IsDeleted == false);
            if (existingUser == null)
            {
                throw new NotFoundException("User not found with id: " + id);
            }
            return existingUser;
        }

        public async Task<Users> UpdateUsers(Users users)
        {
            var existingUser = await _repo.FindOneByCondition(u => u.Id == users.Id && u.IsDeleted == false);
            if (existingUser == null)
            {
                throw new NotFoundException("User not found with id: " + users.Id);
            }
            

            existingUser.FullName = users.FullName;
            existingUser.Dob = users.Dob;
            existingUser.Address = users.Address;
            existingUser.Hobby = users.Hobby;
            existingUser.LastLogin = users.LastLogin;
            existingUser.LearnHourPerDay = users.LearnHourPerDay;
            existingUser.Strength = users.Strength;
            existingUser.AvatarUrl = users.AvatarUrl;


            await _repo.UpdateAsync(existingUser);

            return existingUser;

        }

        public async Task<bool> ChangePassword(ChangePasswordDTO  changePasswordDTO)
        {
            var user = await _repo.FindOneByCondition(u => u.Email == changePasswordDTO.Email && !u.IsDeleted);

            if(user == null)
            {
                throw new NotFoundException($"{changePasswordDTO.Email} was not found");
            }

            if(string.IsNullOrEmpty(changePasswordDTO.NewPassword))
            {
                throw new NotFoundException($"New Password was not valid");
            }

            var passwordHelper = new PasswordHelper();
            var newPasswordHash = passwordHelper.HashPassword(changePasswordDTO.NewPassword);

            user.Password = newPasswordHash;
            await _repo.UpdateAsync(user);
            return true;

        }

        public async Task<Users> CreateOTP(string email)
        {
            var user = await _repo.FindOneByCondition(u => u.Email == email && !u.IsDeleted);
            if (user == null)
            {
                throw new NotFoundException($"{email} was not found");
            }
            var OTP = EmailHelper.GenerateOTP(6);
            user.OTP = OTP;
            await _repo.UpdateAsync(user);
            return user;
        }

        public async Task<bool> CheckOTP(checkOTPDTO checkOTPDTO)
        {
            var user = await _repo.FindOneByCondition(u => u.Email == checkOTPDTO.Email && !u.IsDeleted);
            if (user == null)
            {
                throw new NotFoundException($"{checkOTPDTO.Email} was not found");
            }

            if (user.OTP == null)
            {
                return false;
            }

            if (user.OTP != checkOTPDTO.OTP)
            {
                return false;
            }

            user.OTP = null;
            await _repo.UpdateAsync(user);
            return true;
        }
    }
}
