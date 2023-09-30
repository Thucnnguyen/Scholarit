using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface ICourseService
    {
        Task<Course> UpdateCourse(Course course);
        Task<bool> DeleteCourse(int id);
        Task<int> AddCourse(Course course);
        Task<Course> GetCourseByID(int id);
        Task<PagingResultDTO<Course>> GetAllCourses(int pageNo,int pageSize);

    }
}
