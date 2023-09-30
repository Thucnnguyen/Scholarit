using Scholarit.Data.Repository;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepo _repo;
        public OrderDetailService(IOrderDetailRepo repo)
        {
            _repo = repo;
        }

        public async Task<int> AddOrderDetails(OrderDetail orderDetail)
        {
            var newOrderDetailId = await _repo.CreateAsync(orderDetail);
            return newOrderDetailId;
        }

        public async Task AddOrderDetails(List<OrderDetail> orderDetail)
        {
            await _repo.CreateRangeAsync(orderDetail);
        }

        public async Task<bool> DeleteOrderDetailById(int id)
        {
            var existingOrderDetail = await GetOrderDetailById(id);
            existingOrderDetail.IsDeleted = true;
            await _repo.UpdateAsync(existingOrderDetail);
            return true;

        }

        public async Task<IEnumerable<OrderDetail>> GetAllByOrderId(int orderId)
        {
            var orderDetail = await _repo.GetAllByConditionAsync(_ => _.OrderId == orderId && !_.IsDeleted, _ => _.Id, false);
            return orderDetail;
        }

        public async Task<OrderDetail> GetOrderDetailById(int id)
        {
            var orderDetail = await _repo.FindOneByCondition(_ => _.Id == id && !_.IsDeleted);
            return orderDetail;
        }

        public async Task<OrderDetail> UpdateOrderDetail(OrderDetail orderDetail)
        {
            var existingOrderDetail = await GetOrderDetailById(orderDetail.Id);

            if (orderDetail.FeedBack != null) existingOrderDetail.FeedBack = orderDetail.FeedBack;

            if (orderDetail.Rate != null) existingOrderDetail.Rate = orderDetail.Rate;

            await _repo.UpdateAsync(existingOrderDetail);
            return existingOrderDetail;
        }
    }
}
