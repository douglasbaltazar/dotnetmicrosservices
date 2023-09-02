using GeekShopping.CartAPI.Data.DTO;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;
        public CartController(ICartRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartDTO>> FindById(string userId)
        {
            var cart = await _repository.FindCartByUserId(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
        [HttpPost("add-cart/{id}")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
        {
            var cart = await _repository.SaveOrUpdateCart(cartDTO);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPut("update-cart/{id}")]
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
        {
            var cart = await _repository.SaveOrUpdateCart(cartDTO);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartDTO>> RemoveCart(int id)
        {
            var status = await _repository.RemoveFromCart(id);
            if (!status)
            {
                return BadRequest();
            }
            return Ok(status);
        }

    }
}
