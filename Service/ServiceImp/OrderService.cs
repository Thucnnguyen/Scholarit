using AlumniProject.Data.Repostitory;
using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;
using System.Security.Cryptography.X509Certificates;

namespace Scholarit.Service.ServiceImp
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _repo;
        private readonly IUserService _userService;
        public OrderService(IOrderRepo repo, IUserService userService)
        {
            _repo = repo;
            _userService = userService;
        }

        public async Task<int> AddOrder(Order order)
        {
            var newOrderId = await  _repo.CreateAsync(order);
            return newOrderId;
        }

        public async Task<bool> DeleteOrderById(int id)
        {
            var existingOrder = await GetOrderById(id);
            existingOrder.IsDeleted = true;
            await _repo.UpdateAsync(existingOrder);
            return true;

        }

        public async Task<PagingResultDTO<Order>> GetAllByUserId(int pageNo, int pageSize, int userId)
        {
            var orderListByUserId = await _repo.GetAllByConditionAsync(pageNo, pageSize,
                _ => _.UserId == userId, _ => _.Id, false);
            return orderListByUserId;
        }

        public async Task<PagingResultDTO<Order>> GetAllOrders(int pageNo, int pageSize)
        {
            var orders = await _repo.GetPaginationAsync(pageNo, pageSize);
            return orders;
        }

        public async Task<Order> GetOrderById(int id)
        {
            var existingOrder = await _repo.FindOneByCondition(_ => _.Id == id && !_.IsDeleted);
            return existingOrder != null ? existingOrder : throw new NotFoundException("Order not found with id: " + id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var orders = await _repo.GetAllAsync();

            return orders;  
        }

        public async Task<Order> UpdateOrderStatus(Order order)
        {
            var existingOrder = await GetOrderById(order.Id);

            existingOrder.Status = order.Status;

            await _repo.UpdateAsync(existingOrder);
            return existingOrder;
        }
    }
}
