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
		Task<PagingResultDTO<Course>> GetAllCoursesByUserId(int pageNo, int pageSize, int userId);

		Task<PagingResultDTO<Course>> GetAllFinishedCourses(int pageNo, int pageSize,int userId);
        Task<PagingResultDTO<Course>> GetAllRelatedCourse(int pageNo, int pageSize, int userId);
        Task<PagingResultDTO<Course>> GetAllCourseWithMostView(int pageNo, int pageSize);


		Task<bool> UpdateNote(int courseId, int userId, string note);

        Task<PagingResultDTO<Course>> GetAllCoursesByCategoriesId(int pageNo, int pageSize,int categoryId);
        Task<PagingResultDTO<Course>> SearchCourseByName(int pageNo, int pageSize,string searchString);
    }
}
