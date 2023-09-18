using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class ResourceRepo : RepositoryBase<Resource>, IResourceRepo
    {
        public ResourceRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
