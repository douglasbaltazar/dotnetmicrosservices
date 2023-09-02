namespace GeekShopping.CartAPI.Data.DTO
{
    public class CartDTO
    {
        public CartHeaderDTO CartHeader { get; set; }
        public IEnumerable<CartDetailDTO> CartDetail { get; set; }
    }
}
