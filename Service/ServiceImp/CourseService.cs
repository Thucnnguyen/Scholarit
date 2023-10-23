using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;
using System.Security.Cryptography.X509Certificates;

namespace Scholarit.Service.ServiceImp
{
	public class CourseService : ICourseService
    {
        private readonly ICourseRepo _repo;
        private readonly ICategoryService _categoryService;
        private readonly IEnrollService _enrollService;
        public CourseService(ICourseRepo repo, ICategoryService categoryService, IEnrollService enrollService)
        {
            _repo = repo;
            _categoryService = categoryService;
            _enrollService = enrollService;
        }

        public async Task<int> AddCourse(Course course)
        {
            var category = await _categoryService.GetCategoryByID(course.CategoryId);
            int id = await _repo.CreateAsync(course);
            await _categoryService.updateTotalCourseCateogry(category.Id, 1);
            return id;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            var course = await GetCourseByID(id);
            await _repo.DeleteByIdAsync(id);
			var category = await _categoryService.GetCategoryByID(course.CategoryId);
			await _categoryService.updateTotalCourseCateogry(category.Id, -1);

			return true;
        }

        public Task<PagingResultDTO<Course>> GetAllCourses(int pageNo, int pageSize)
        {
            var courseList = _repo.GetAllByConditionAsync(pageNo, pageSize, c => !c.IsDeleted, c => c.DateCreated, true);
            return courseList;
        }

        public Task<PagingResultDTO<Course>> GetAllCoursesByCategoriesId(int pageNo, int pageSize, int categoryId)
        {
            var courseList = _repo.GetAllByConditionAsync(pageNo, pageSize, c => !c.IsDeleted && c.CategoryId == categoryId, c => c.DateCreated, true);
            return courseList;
        }

        public async Task<PagingResultDTO<Course>> GetAllFinishedCourses(int pageNo, int pageSize, int userId)
        {
            var enrollList = await _enrollService.GetFinishedCousre(pageNo, pageSize, userId);
            var courseListTask = enrollList.Items.Select(async e => await GetCourseByID(e.CourseId));
            var courseList = await Task.WhenAll(courseListTask);
            var CourseResult = new PagingResultDTO<Course>()
            {
                CurrentPage = pageNo,
                PageSize = pageSize,
                Items = courseList.ToList(),
                TotalItems = courseList.Count()
            };
            return CourseResult;
        }

		public async Task<PagingResultDTO<Course>> GetAllCoursesByUserId(int pageNo, int pageSize, int userId)
		{
			var enrollList = await _enrollService.GetAllCousre(pageNo, pageSize, userId);
			var courseListTask = enrollList.Items.Select(async e => await GetCourseByID(e.CourseId));
			var courseList = await Task.WhenAll(courseListTask);
			var CourseResult = new PagingResultDTO<Course>()
			{
				CurrentPage = pageNo,
				PageSize = pageSize,
				Items = courseList.ToList(),
				TotalItems = courseList.Count()
			};
			return CourseResult;
		}

		public async Task<PagingResultDTO<Course>> GetAllRelatedCourse(int pageNo, int pageSize, int userId)
        {
            var latestEnroll = await _enrollService.GetlatestEnrollByUserId(userId);
            if (latestEnroll == null) { return new PagingResultDTO<Course>(); }
            var latestCourse = await GetCourseByID(latestEnroll.CourseId);
            var ListOfCourseRelated = await _repo.GetAllByConditionAsync(pageNo, pageSize, c => c.CategoryId == latestCourse.CategoryId && !c.IsDeleted, c => c.DateCreated, true);
            return ListOfCourseRelated;
        }

        public async Task<Course> GetCourseByID(int id)
        {
            var course = await _repo.FindOneByCondition(c => c.Id == id && c.IsDeleted == false);
            await updateCourseView(course);

			return course != null ? course : throw new NotFoundException("Course not found with Id: " + id);
        }

        private async Task updateCourseView(Course course)
        {
            course.View+=1;
            await _repo.UpdateAsync(course);
        }

        public Task<PagingResultDTO<Course>> SearchCourseByName(int pageNo, int pageSize, string searchString)
        {
            var courseList = _repo.GetAllByConditionAsync(pageNo, pageSize, c => !c.IsDeleted && c.Name.Contains(searchString), c => c.DateCreated, true);
            return courseList;
        }



        public async Task<Course> UpdateCourse(Course course)
        {
            var existingCourse = await GetCourseByID(course.Id);

            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;
            existingCourse.Duration = course.Duration;
            existingCourse.Author = course.Author;
            existingCourse.Price = course.Price;
            existingCourse.Discount = course.Discount;
            existingCourse.NumberOfChapter = course.NumberOfChapter;
            existingCourse.CategoryId = course.CategoryId;

            await _repo.UpdateAsync(existingCourse);
            return existingCourse;
        }

        public async Task<bool> UpdateNote(int courseId, int userId, string note)
        {
            var enroll = await _enrollService.GetEnrollByUserIdAndCourseId(userId, courseId);
            await _enrollService.UpdateNote(enroll, note);
            return true;
        }

		public Task<PagingResultDTO<Course>> GetAllCourseWithMostView(int pageNo, int pageSize)
		{
			var courseList = _repo.GetAllByConditionAsync(pageNo, pageSize, c => !c.IsDeleted, c => c.View, true);
			return courseList;
		}
	}
}
