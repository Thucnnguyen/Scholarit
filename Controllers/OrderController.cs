using AlumniProject.Dto;
using AlumniProject.Ultils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Service;
using Scholarit.Utils;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IMapper _mapper;
        private readonly TokenUltil tokenUltil;
        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService, IMapper mapper)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _mapper = mapper;
            this.tokenUltil = new TokenUltil();
        }

        // GET: api/<OrderService>
        [HttpGet("user/order"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<PagingResultDTO<OrderDTO>>> GetOrderByUserId(
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
            )
        {
            var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;
            var orders = await _orderService.GetAllByUserId(pageNo, pageSize, int.Parse(userId));
            var orderDTO = orders.Items.Select(o => _mapper.Map<OrderDTO>(o)).ToList();
            return Ok(new PagingResultDTO<OrderDTO>()
            {
                CurrentPage = pageNo,
                Items = orderDTO,
                PageSize = pageSize,
                TotalItems = orderDTO.Count
            });
        }

        [HttpGet("admin/order"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<PagingResultDTO<OrderDTO>>> GetOrderForAdmin(
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
            )
        {
            var orders = await _orderService.GetAll(pageNo, pageSize);
            var orderDTO = orders.Items.Select(o => _mapper.Map<OrderDTO>(o)).ToList();
            return Ok(new PagingResultDTO<OrderDTO>()
            {
                CurrentPage = pageNo,
                Items = orderDTO,
                PageSize = pageSize,
                TotalItems = orderDTO.Count
            });
        }

        // GET api/<OrderService>/5
        [HttpGet("user/order/{orderId}"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<OrderAndDetailDTO>> Get([FromRoute] int orderId)
        {
            var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;
            var order = await _orderService.GetOrderById(orderId,int.Parse(userId));
            return Ok(order);
        }

		[HttpGet("admin/order/{orderId}"), Authorize(Roles = "admin")]
		public async Task<ActionResult<OrderDTO>> GetOrderForAdmin([FromRoute] int orderId)
		{
			var order = await _orderService.GetOrderById(orderId);
			return Ok(_mapper.Map<OrderDTO>(order));
		}

		// POST api/<OrderService>
		[HttpPost("user/order"), Authorize(Roles = "admin,user")]
        public async Task<ActionResult<int>> Post([FromBody] OrderAddDTO orderAddDTO)
        {
            var userId = tokenUltil.GetClaimByType(User, Enums.UserId).Value;

            var newOrderID = await _orderService.AddOrder(orderAddDTO,int.Parse(userId));
            return Ok(newOrderID);
        }

        // PUT api/<OrderService>/5
        [HttpPut("admin/order"), Authorize(Roles = "admin")]
        public async Task<ActionResult<OrderDTO>> Put([FromBody] OrderUpdateStatusDTO orderUpdateStatusDTO)
        {
            var order = await _orderService.UpdateOrderStatus(orderUpdateStatusDTO);
            return Ok(_mapper.Map<OrderDTO>(order));
        }

        // DELETE api/<OrderService>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
