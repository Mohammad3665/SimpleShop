using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        //Simulation: Payment is always successful.
        public Task<PaymentResult> ProcessPaymentAsync(decimal amount, int orderId, string userId, CancellationToken cancellationToken)
        {
            // In the real project, the code for connecting to the banking portal would be placed here.

            var result = new PaymentResult
            {
                IsSuccessful = true,
                Message = "Payment was successful.",
                TransactionId = Guid.NewGuid().ToString().Substring(0, 8)
            };
            return Task.FromResult(result);
        }
    }
}
