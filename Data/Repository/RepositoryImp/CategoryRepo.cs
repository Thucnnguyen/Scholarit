using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class CategoryRepo : RepositoryBase<Category>, ICategoryRepo
    {
        public CategoryRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
