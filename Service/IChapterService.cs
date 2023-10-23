using AlumniProject.Dto;
using Scholarit.DTO;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IChapterService
    {
        Task<IEnumerable<Chapter>> GetAllByCourseId(int courseId, bool desdescending);
        Task<Chapter> UpdateChapter(Chapter chapter);
        Task<bool> DeleteChapterById(int id);
        Task<int> AddChapter(Chapter chapter);
        Task<Chapter> GetChapterByID(int id);
        Task<CourseProcessingDTO> GetCurrentCourseProcessing(int userId);

		Task<bool> UpdateChapterForEnroll(int userId, int courseId, int chapterId);

	}
}
