using AlumniProject.Data.Repostitory.RepositoryImp;
using AlumniProject.Dto;
using Microsoft.EntityFrameworkCore;
using Scholarit.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class OrderRepo : RepositoryBase<Order>, IOrderRepo
    {
        public OrderRepo(ScholaritDbContext context) : base(context)
        {
        }
        public override async Task<PagingResultDTO<Order>> GetAllByConditionAsync(int pageNo,
            int pageSize,
            Expression<Func<Order, bool>> filter,
            Expression<Func<Order, object>> orderBy,
            bool descending = false)
        {
            var skipAmount = (pageNo - 1) * pageSize;
            var query = _context.Set<Order>().Include(o => o.OrderDetail).Include(o => o.User).AsQueryable();

            query = query.Where(filter);

            if (orderBy != null)
            {
                if (descending)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else { query = query.OrderBy(orderBy); }
            }

            var entities = await query.Skip(skipAmount).Take(pageSize).ToListAsync();
            var total = await query.CountAsync();
            var resultt = new PagingResultDTO<Order>
            {
                CurrentPage = pageNo,
                Items = entities,
                PageSize = pageSize,
                TotalItems = total
            };
            return resultt;

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


        public override async Task<Order> FindOneByCondition(Expression<Func<Order, bool>> filter, params Expression<Func<Order, object>>[] includeProperties)
        {
            var query = _context.Set<Order>().AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            query = query.Where(filter);

            var entities = await query.Include(o => o.User).Include(o => o.OrderDetail).FirstOrDefaultAsync();
            return entities;
        }
    }
}
