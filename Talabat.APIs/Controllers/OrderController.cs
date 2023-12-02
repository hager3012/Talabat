using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Servicies.Contract;

namespace Talabat.APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : BaseAPIController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrderController(IOrderServices orderServices,IMapper mapper)
        {
            _orderServices = orderServices;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CraeteOrder(OrderDto order)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var mapperAddress = _mapper.Map<AddressDto, Address>(order.shippingAddress);
            var result = await _orderServices.CreateOrderAsync(email, order.BasketId, order.DeliveryMethodId, mapperAddress);
            if (result is null) return BadRequest(new ApiResponse(400));
            return Ok(_mapper.Map<Order,OrderToReturnDto>(result));
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order =await _orderServices.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(order));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOredrForUser(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orderRepo=await _orderServices.GetOrderByIdForUserAsync(email, id);
            if (orderRepo is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Order, OrderToReturnDto>(orderRepo));
        }
        [HttpGet("deliverymethods")]
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethods()
        {
            var DeliveryMethod=await _orderServices.GetDeliveryMethodsAsync();
            return DeliveryMethod;
        }
    }
}
