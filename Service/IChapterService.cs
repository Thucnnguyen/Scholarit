using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IChapterService
    {
        Task<PagingResultDTO<Chapter>> GetAllByCourseId(int pageNo, int pageSize, int courseId, bool desdescending);
        Task<Chapter> UpdateChapter(Chapter chapter);
        Task<bool> DeleteChapterById(int id);
        Task<int> AddChapter(Chapter chapter);
        Task<Chapter> GetChapterByID(int id);
    }
}
