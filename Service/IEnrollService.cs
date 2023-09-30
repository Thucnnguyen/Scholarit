using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IEnrollService
    {
        Task<int> AddEnroll(Enroll enroll);
        Task<Enroll> GetEnrollById(int id);
        Task<Enroll> GetEnrollByUserIdAndCourseId(int userId, int orderId);

    }
}
