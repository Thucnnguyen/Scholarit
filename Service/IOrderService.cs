using AlumniProject.Dto;
using Scholarit.DTO;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IOrderService
    {
        Task<PagingResultDTO<Order>> GetAllByUserId(int pageNo,int pageSize,int userId);
        Task<PagingResultDTO<Order>> GetAll(int pageNo, int pageSize);
        Task<Order> UpdateOrderStatus(OrderUpdateStatusDTO order);
        Task<bool> DeleteOrderById(int id);
        Task<int> AddOrder(OrderAddDTO orderAddDTO,int userId);
        Task<OrderAndDetailDTO> GetOrderById(int orderId,int userId);
		Task<Order> GetOrderById(int orderId);

	}
}
