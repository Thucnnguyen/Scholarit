using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class UserRepo : RepositoryBase<Users>, IUserRepo
    {
        public UserRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
