using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<CoupomViewModel> GetCoupon(string code, string token);




    }
}
