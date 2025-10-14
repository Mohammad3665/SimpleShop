using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsDTO>
    {
        private readonly IApplicationDbContext _context;
        public GetOrderDetailsQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<OrderDetailsDTO> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.Id == request.OrderId);
            if (!string.IsNullOrEmpty(request.UserId))
            {
                query = query.Where(o => o.UserId == request.UserId);
            }
            var order = await query.FirstOrDefaultAsync(cancellationToken);
            if (order == null) return null;

            return new OrderDetailsDTO
            {
                OrderId = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                PostalCode = order.PostalCode,
                ShippingAddress = order.ShippingAddress,
                Status = order.Status.ToString(),
                TotalPrice = order.TotalPrice,
                Items = order.Items.Select(oi => new OrderDetailItemDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }
    }
}
