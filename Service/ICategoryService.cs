using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll(bool desdescending);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);
        Task<int> AddCategory(Category category);
        Task<Category> GetCategoryByID (int id);
        Task updateTotalCourseCateogry(int cateogryId, int count);
    }
}
