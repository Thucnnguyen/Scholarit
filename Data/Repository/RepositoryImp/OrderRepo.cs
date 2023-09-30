using AlumniProject.Data.Repostitory.RepositoryImp;
using AlumniProject.Dto;
using Microsoft.EntityFrameworkCore;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class OrderRepo : RepositoryBase<Order>, IOrderRepo
    {
        public OrderRepo(ScholaritDbContext context) : base(context)
        {
        }

        public override async Task<PagingResultDTO<Order>> GetPaginationAsync(int pageNo, int pageSize)
        {
            var skipAmount = (pageNo - 1) * pageSize;
            var query = _context.Set<Order>().Include(o => o.OrderDetail).Include(o => o.User).AsQueryable();

            var entities = await query.Skip(skipAmount).Take(pageSize).ToListAsync();
            var total = await query.CountAsync();
            var result = new PagingResultDTO<Order>
            {
                CurrentPage = pageNo,
                Items = entities,
                PageSize = pageSize,
                TotalItems = total
            };
            return result;
        }


        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Set<Order>().Include(o => o.User).Include(o => o.OrderDetail).ToListAsync();
        }
    }
}
