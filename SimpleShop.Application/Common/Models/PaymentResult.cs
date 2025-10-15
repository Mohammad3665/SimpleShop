
namespace SimpleShop.Application.Common.Models
{
    public class PaymentResult
    {
        public bool IsSuccessful { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }
    }
}
