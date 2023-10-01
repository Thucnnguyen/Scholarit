using AlumniProject.Data.Repostitory.RepositoryImp;
using AlumniProject.Dto;
using Microsoft.EntityFrameworkCore;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class UserRepo : RepositoryBase<Users>, IUserRepo
    {
        public UserRepo(ScholaritDbContext context) : base(context)
        {
        }

        public override async Task<PagingResultDTO<Users>> GetPaginationAsync(int pageNo, int pageSize)
        {
            var skipAmount = (pageNo - 1) * pageSize;
            var query = _context.Set<Users>().Where(u => u.IsDeleted == false).AsQueryable();

            var entities = await query.Skip(skipAmount).Take(pageSize).ToListAsync();
            var total = await query.CountAsync();
            var result = new PagingResultDTO<Users>
            {
                CurrentPage = pageNo,
                Items = entities,
                PageSize = pageSize,
                TotalItems = total
            };
            return result;
        }
    }
}
