﻿using AlumniProject.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Entity;
using Scholarit.Service;
using System.ComponentModel.DataAnnotations;

namespace Scholarit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            var orders = await _orderService.GetOrdersAsync();

            return Ok(_mapper.Map<List<OrderDTO>>(orders));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _orderService.GetOrderById(id);
            if (result is null) return NotFound();
            return Ok(_mapper.Map<OrderDTO>(result));
        }


        [HttpGet("page")]
        public async Task<ActionResult<PagingResultDTO<OrderDTO>>> Get(
             [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
            [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
            )
        {
            var orderList = await _orderService.GetAllOrders(pageNo, pageSize);
            var orderListDTO = orderList.Items.Select(_ => _mapper.Map<OrderDTO>(_));
            return Ok(new PagingResultDTO<OrderDTO>()
            {
                CurrentPage = orderList.CurrentPage,
                PageSize = orderList.PageSize,
                Items = orderListDTO.ToList(),
                TotalItems = orderList.TotalItems
            });
        }

        [HttpGet("by-user/{userid}")]
        public async Task<ActionResult<PagingResultDTO<OrderDTO>>> GetByUserAsync([FromRoute] int userid,
          [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageNo is Required")] int pageNo = 1,
       [FromQuery, Range(1, int.MaxValue), Required(ErrorMessage = "pageSize is Required")] int pageSize = 10
         )
        {
            var orderList = await _orderService.GetAllOrderByUser(userid, pageNo, pageSize);
            var oderListDTO = orderList.Items.Select(_ => _mapper.Map<OrderDTO>(_));
            return Ok(new PagingResultDTO<OrderDTO>()
            {
                CurrentPage = orderList.CurrentPage,
                PageSize = orderList.PageSize,
                Items = oderListDTO.ToList(),
                TotalItems = orderList.TotalItems
            });

        }
    }
}
