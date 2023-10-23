using AlumniProject.Dto;
using Scholarit.DTO;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IEnrollService
    {
        Task<int> AddEnroll(Enroll enroll);
        Task<Enroll> GetEnrollById(int id);
        Task<Enroll> GetEnrollByUserIdAndCourseId(int userId, int courseID);
        Task<Enroll> GetlatestEnrollByUserId(int userId);
		Task<PagingResultDTO<Enroll>> GetAllCousre(int pageNo, int pageSize, int userId);

		Task<Enroll> UpdateChapterId(Enroll enroll,int userId);
        Task<Enroll> UpdateNote(Enroll enroll, string note);

        Task<PagingResultDTO<Enroll>> GetFinishedCousre(int pageNo, int pageSize, int userId);

    }
}
