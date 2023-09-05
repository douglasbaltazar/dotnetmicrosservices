using GeekShopping.CartAPI.Data.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text.Json;

namespace GeekShopping.CartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _httpClient;

        public CouponRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CouponDTO> GetCouponByCouponCode(string couponCode, string token)
        {
            //         public const string BasePath = "api/v1/coupon";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"/api/v1/coupon/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK) return new CouponDTO();
            return JsonSerializer.Deserialize<CouponDTO>(content,
                new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
            /*
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"/api/v1/coupon/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new CouponDTO();
            }
            return JsonSerializer.Deserialize<CouponDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            */
        }
    }
}
