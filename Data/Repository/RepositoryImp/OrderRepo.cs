using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class OrderRepo : RepositoryBase<Order>, IOrderRepo
    {
        public OrderRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
