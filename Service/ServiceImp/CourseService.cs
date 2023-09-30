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
        public CourseService(ICourseRepo repo, ICategoryService categoryService)
        {
            _repo = repo;
            _categoryService = categoryService;
        }

        public async Task<int> AddCourse(Course course)
        {
            var category = await _categoryService.GetCategoryByID(course.CategoryId);
            int id = await _repo.CreateAsync(course);
            return id;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            var course = GetCourseByID(id);
            await _repo.DeleteByIdAsync(id);
            return true;
        }

        public Task<PagingResultDTO<Course>> GetAllCourses(int pageNo, int pageSize)
        {
            var courseList = _repo.GetAllByConditionAsync(pageNo,pageSize,c => !c.IsDeleted, c=> c.DateCreated,true);
            return courseList;
        }

        public async Task<Course> GetCourseByID(int id)
        {
            var course = await _repo.FindOneByCondition(c =>c.Id == id && c.IsDeleted == false);
            return course != null ? course : throw new NotFoundException("Course not found with Id: "+ id);
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
    }
}
