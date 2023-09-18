using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class EnrollRepo : RepositoryBase<Enroll>, IEnrollRepo
    {
        public EnrollRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
