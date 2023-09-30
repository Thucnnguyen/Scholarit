using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetail>> GetAllByOrderId( int userId);
        Task<OrderDetail> UpdateOrderDetail(OrderDetail orderDetail);
        Task<bool> DeleteOrderDetailById(int id);
        Task<int> AddOrderDetails(OrderDetail orderDetail);
        Task AddOrderDetails(List<OrderDetail> orderDetails);

        Task<OrderDetail> GetOrderDetailById(int id);
    }
}
