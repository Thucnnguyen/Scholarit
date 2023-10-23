using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.DTO;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
	public class EnrollService : IEnrollService
    {
        private readonly IEnrollRepo _repo;
        private readonly IChapterRepo chapterRepo;

        public EnrollService(IEnrollRepo enrollRepo,IChapterRepo chapterRepo)
        {
            this._repo = enrollRepo;
            this.chapterRepo = chapterRepo;
        }

        public async Task<int> AddEnroll(Enroll enroll)
        {
            var newId = await _repo.CreateAsync(enroll);
            return newId;
        }

		public async Task<PagingResultDTO<Enroll>> GetAllCousre(int pageNo, int pageSize, int userId)
		{
			var finishedCourses = await _repo.GetAllByConditionAsync(pageNo, pageSize, e => e.UserId == userId && !e.IsDeleted, e => e.DateCreated, true);
			return finishedCourses;
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

        public async Task<PagingResultDTO<Enroll>> GetFinishedCousre(int pageNo,int pageSize,int userId)
        {
            var finishedCourses = await _repo.GetAllByConditionAsync(pageNo, pageSize, e => e.IsFinished && e.UserId == userId && !e.IsDeleted, e => e.DateCreated,true);
            return finishedCourses;
        }

        public async Task<Enroll> GetlatestEnrollByUserId(int userId)
        {
            var enroll = await _repo.GetAllByConditionAsync(e => e.UserId == userId && !e.IsDeleted, e => e.DateCreated, true);
            var latestEnroll = enroll.FirstOrDefault();
            if (latestEnroll == null) throw new NotFoundException("Don't have any course enroll");
            return latestEnroll;
        }

        public async Task<Enroll> UpdateChapterId(Enroll enroll,int userId)
        {
            var existingEnroll = await _repo.FindOneByCondition(_ => _.UserId == userId && _.CourseId == enroll.CourseId && !_.IsDeleted);

            existingEnroll.ChapterId = enroll.ChapterId;
            var chapters = await chapterRepo.GetAllByConditionAsync(c => c.CourseId == enroll.CourseId && !c.IsDeleted, c => c.Order, true);
            var lastChapter = chapters.FirstOrDefault();
            if(lastChapter != null)
            {
                if (lastChapter.Id == enroll.ChapterId)
                {
                    existingEnroll.IsFinished = true;
                }
            }
            

            await _repo.UpdateAsync(existingEnroll);

            return existingEnroll;
        }

        public async Task<Enroll> UpdateNote(Enroll enroll, string note)
        {
            var existingEnroll = await _repo.FindOneByCondition(_ => _.UserId == enroll.UserId && _.CourseId == enroll.CourseId && !_.IsDeleted);
            existingEnroll.Note = note;

            await _repo.UpdateAsync(existingEnroll);

            return existingEnroll;
        }
    }
}
