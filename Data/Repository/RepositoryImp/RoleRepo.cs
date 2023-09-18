using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class RoleRepo : RepositoryBase<Role>, IRoleRepo
    {
        public RoleRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
