using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.DTO;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _repo;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IEnrollService _enrollService;
        private readonly ICourseService _courseService;
        private readonly IChapterService _chapterService;
        public OrderService(IOrderRepo repo, IOrderDetailService orderDetailService, IEnrollService enrollService, ICourseService courseService, IChapterService chapterService)
        {
            _repo = repo;
            _orderDetailService = orderDetailService;
            _enrollService = enrollService;
            _courseService = courseService;
            _chapterService = chapterService;
        }

        public async Task<int> AddOrder(OrderAddDTO order, int userId)
        {
            var newOrderId = await _repo.CreateAsync(new Order()
            {
                Note = order.Note,
                Price = order.Price,
                Status = 1,
                UserId = userId,
            });

            foreach (var od in order.OrderDetails)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    CourseId = od.CourseId,
                    CourseName = od.CourseName,
                    FeedBack = od.FeedBack,
                    OrderId = newOrderId,
                    Rate = od.Rate,
                };
                var orderdetailId = await _orderDetailService.AddOrderDetails(orderDetail);
            }
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

        public async Task<PagingResultDTO<Order>> GetAll(int pageNo, int pageSize)
        {
            var orderListByUserId = await _repo.GetAllByConditionAsync(pageNo, pageSize, _ => _.IsDeleted == false, _ => _.DateCreated, true);
            return orderListByUserId;
        }

        public async Task<Order> GetOrderById(int id)
        {
            var existingOrder = await _repo.FindOneByCondition(_ => _.Id == id && !_.IsDeleted);
            return existingOrder != null ? existingOrder : throw new NotFoundException("Order not found with id: " + id);
        }
        public async Task<OrderAndDetailDTO> GetOrderById(int orderId, int userId)
        {
            var existingOrder = await _repo.FindOneByCondition(_ => _.Id == orderId && _.UserId == userId && !_.IsDeleted, _ => _.OrderDetail);
            if (existingOrder == null) throw new NotFoundException("Order not found with id: " + orderId);
			var orderAndDetailService = new OrderAndDetailDTO()
            {
                OrderId = orderId,
                UserId = userId,
                DateCreated = existingOrder.DateCreated,
                Note = existingOrder.Note,
                Price = existingOrder.Price,
                Status = existingOrder.Status,
                Items = existingOrder.OrderDetail.Select(_ => new OrderDetailDTO()
                {
                    Id = _.Id,
                    CourseId = _.CourseId,
                    CourseName = _.CourseName,
                    FeedBack = _.FeedBack,
                    OrderId = _.OrderId,
                    Rate = _.Rate
                }).ToList(),
            };
            return existingOrder != null ? orderAndDetailService : throw new NotFoundException("Order not found with id: " + orderId);
        }

        public async Task<Order> UpdateOrderStatus(OrderUpdateStatusDTO orderUpdateStatus)
        {
            var existingOrder = await GetOrderById(orderUpdateStatus.OrderId);

            existingOrder.Status = orderUpdateStatus.Status;
            var orderdetail = await _orderDetailService.GetAllByOrderId(orderUpdateStatus.OrderId);

            if (orderUpdateStatus.Status == 2)
            {
                foreach (var item in orderdetail)
                {
                    var chapter = await _chapterService.GetAllByCourseId(item.CourseId, false);
                    var a = await _enrollService.AddEnroll(new Enroll()
                    {
                        ChapterId = chapter.First().Id,
                        UserId = existingOrder.UserId,
                        CourseId = item.CourseId,
                        OrderDetailId = item.Id,
                        Note = ""
                    });
                }
            }
            await _repo.UpdateAsync(existingOrder);
            return existingOrder;
        }
    }
}
