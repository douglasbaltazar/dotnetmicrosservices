using GeekShopping.CartAPI.Data.DTO;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQSender;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;
        private readonly ICouponRepository _couponRepository;
        private readonly IRabbitMQMessageSender _rabbitMQSender;

        public CartController(ICartRepository repository, ICouponRepository couponRepository, IRabbitMQMessageSender rabbitMQSender)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
            _rabbitMQSender = rabbitMQSender ?? throw new ArgumentNullException(nameof(rabbitMQSender));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartDTO>> FindById(string id)
        {
            var cart = await _repository.FindCartByUserId(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }
        [HttpPost("add-cart")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
        {
            var cart = await _repository.SaveOrUpdateCart(cartDTO);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPut("update-cart")]
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

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO cartDTO)
        {
            var status = await _repository.ApplyCoupon(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);
            if (!status)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartDTO>> RemoveCoupon(string userId)
        {
            var status = await _repository.RemoveCoupom(userId);
            if (!status)
            {
                return NotFound();
            }
            return Ok(status);
        }
        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO dto)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            // string token = Request.Headers.Authorization;
            if(dto?.UserId== null)
            {
                return BadRequest();
            }
            var cart = await _repository.FindCartByUserId(dto.UserId);
            if (cart == null)
            {
                return NotFound();
            }
            if(!string.IsNullOrEmpty(dto.CouponCode))
            {
                CouponDTO coupon = await _couponRepository.GetCouponByCouponCode(dto.CouponCode, token);
                if(dto.DiscountTotal != coupon.DiscountAmount)
                {
                    return StatusCode(412);
                }
            }
            dto.CartDetails = cart.CartDetail;
            dto.Time = DateTime.Now;

            // Task RabbitMQ logic comes here
            _rabbitMQSender.SendMessage(dto, "checkoutqueue");
            await _repository.ClearCart(dto.UserId);
            return Ok(dto);
        }

    }
}
