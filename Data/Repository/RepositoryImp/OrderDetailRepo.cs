using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class OrderDetailRepo : RepositoryBase<OrderDetail>, IOrderDetailRepo
    {
        public OrderDetailRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
