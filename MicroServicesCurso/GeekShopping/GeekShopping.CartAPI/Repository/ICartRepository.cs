using GeekShopping.CartAPI.Data.DTO;

namespace GeekShopping.CartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartDTO> FindCartByUserId(string userId);
        Task<CartDTO> SaveOrUpdateCart(CartDTO cart);

        Task<bool> RemoveFromCart(long cartDetailsId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupom(string userId);

        Task<bool> ClearCard(string userId);
    }
}
