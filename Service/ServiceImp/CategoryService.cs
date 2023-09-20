using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _repo;
        public CategoryService(ICategoryRepo categoryRepo)
        {
            _repo = categoryRepo;
        }
        public async Task<int> AddCategory(Category category)
        {
            var NewCategoryId = await _repo.CreateAsync(category);
            return NewCategoryId;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var Category = await GetCategoryByID(id);

            await _repo.DeleteByIdAsync(id);

            return true;
        }

        public async Task<IEnumerable<Category>> GetAll(bool descending)
        {
            var courseList = await _repo.GetAllByConditionAsync(c => c.IsDeleted == false, c => c.Id, descending);
            return courseList;
        }

        public async Task<Category> GetCategoryByID(int id)
        {
            var category = await _repo.FindOneByCondition(c => c.Id == id && c.IsDeleted == false);
            return category != null ? category : throw new NotFoundException("category not found with id: " + id);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var cate = await GetCategoryByID(category.Id);
            cate.Name = category.Name;
             await _repo.UpdateAsync(cate);
            return category;
        }
    }
}
