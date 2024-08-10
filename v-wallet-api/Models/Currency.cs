namespace v_wallet_api.Models
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
    }
}