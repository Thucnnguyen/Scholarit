using AlumniProject.Data.Repostitory.RepositoryImp;
using Microsoft.EntityFrameworkCore;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class RoleRepo : RepositoryBase<Role>, IRoleRepo
    {
        public RoleRepo(ScholaritDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Set<Role>().Where(r => r.IsDeleted == false).ToListAsync();
        }

    }
}
