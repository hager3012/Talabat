using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contract;

namespace Talabat.APIs.Controllers
{
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo,IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<customerBasket>> GetBasket(string id)
        {
            var basket=await _basketRepo.GetBasketById(id);
            if (basket == null) return new customerBasket(id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<customerBasket>> UpdateBasket(customerBasketDto basket)
        {
            var basketMapper=_mapper.Map<customerBasketDto,customerBasket>(basket);
           var createdOrUpdateBasket= await _basketRepo.UpdateBasket(basketMapper);
            if (createdOrUpdateBasket == null) return BadRequest(new ApiResponse(400));
            return Ok(createdOrUpdateBasket);

        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepo.DeleteBasket(id);
        }
    }
}
