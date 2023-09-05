namespace GeekShopping.OrderAPI.Messages
{
    public class UpdatePaymentResultDTO
    {
        public long OrderId { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
