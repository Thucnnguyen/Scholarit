using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class EnrollService : IEnrollService
    {
        private readonly IEnrollRepo _repo;
        public EnrollService(IEnrollRepo enrollRepo)
        {
            this._repo = enrollRepo;
        }

        public async Task<int> AddEnroll(Enroll enroll)
        {
            var newId = await _repo.CreateAsync(enroll);
            return newId;
        }

        public async Task<Enroll> GetEnrollById(int id)
        {
            var existingEnroll = await _repo.FindOneByCondition(_ => _.Id == id && !_.IsDeleted);
            return existingEnroll != null ? existingEnroll : throw new NotFoundException("Enoll not found with id: " + id);
        }

        public async Task<Enroll> GetEnrollByUserIdAndCourseId(int userId, int courseId)
        {
            var existingEnroll = await _repo.FindOneByCondition(_ => _.UserId == userId && _.CourseId == courseId && !_.IsDeleted);
            return existingEnroll != null ? existingEnroll : throw new NotFoundException("Enoll not found with courseId: " + courseId);
        }
    }
}
