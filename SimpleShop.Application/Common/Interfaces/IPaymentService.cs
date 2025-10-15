using SimpleShop.Application.Common.Models;

namespace SimpleShop.Application.Common.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPaymentAsync(decimal  amount, int orderId, string userId, CancellationToken cancellationToken);
    }
}
