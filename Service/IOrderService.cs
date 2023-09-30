using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IOrderService
    {
        Task<PagingResultDTO<Order>> GetAllByUserId(int pageNo,int pageSize,int userId);
        Task<Order> UpdateOrderStatus(Order order);
        Task<bool> DeleteOrderById(int id);
        Task<int> AddOrder(Order order);
        Task<Order> GetOrderById(int id);
    }
}
