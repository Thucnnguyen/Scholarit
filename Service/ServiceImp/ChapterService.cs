using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepo _repo;
        private readonly ICourseService _courseService;
        public ChapterService(IChapterRepo repo, ICourseService courseService)
        {
            _repo = repo;
            _courseService = courseService;
        }

        public async Task<int> AddChapter(Chapter chapter)
        {
            var course = await _courseService.GetCourseByID(chapter.CourseId);
            var NewChapterId = await _repo.CreateAsync(chapter);
            return NewChapterId;
        }

        public async Task<bool> DeleteChapterById(int id)
        {
            var chapter = await GetChapterByID(id);

            await _repo.DeleteByIdAsync(id);
            return true;
        }

        public async Task<PagingResultDTO<Chapter>> GetAllByCourseId(int pageNo, int pageSize, int courseId, bool desdescending)
        {
            var chapter = await _repo.GetAllByConditionAsync(pageNo, pageSize, c => c.CourseId == courseId && !c.IsDeleted, c => c.Id, desdescending);
            return chapter != null ? chapter : throw new NotFoundException("Chapter Not found with courseId: " + courseId);
        }

        public async Task<Chapter> GetChapterByID(int id)
        {
            var chapter = await _repo.FindOneByCondition(c => c.Id == id && !c.IsDeleted);
            return chapter != null ? chapter : throw new NotFoundException("Chapter not found with id: " + id);
        }

        public async Task<Chapter> UpdateChapter(Chapter updatedChapter)
        {
            var existingChapter = await GetChapterByID(updatedChapter.Id);
            existingChapter.Name = updatedChapter.Name;
            existingChapter.Description = updatedChapter.Description;
            existingChapter.Content = updatedChapter.Content;
            existingChapter.Duration = updatedChapter.Duration;
            existingChapter.Order = updatedChapter.Order;
            existingChapter.CourseId = updatedChapter.CourseId;

            await _repo.UpdateAsync(existingChapter);
            return existingChapter;
        }
    }
}
