using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Domain.Enums;
using System;

namespace SimpleShop.Application.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public UpdateOrderStatusCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
            if (order == null)
            {
                throw new Exception($"Order with order id {request.OrderId} not found.");
            }
            var oldStatus = order.Status;
            order.Status = request.NewOrderStatus;

            if (oldStatus != OrderStatus.Cancelled && request.NewOrderStatus != OrderStatus.Cancelled)
            {
                var orderItems = await _context.OrderItems
                    .Include(oi => oi.Product)
                    .Where(oi => oi.OrderId == request.OrderId)
                    .ToListAsync(cancellationToken);

                foreach ( var item in orderItems )
                {
                    item.Product.Stock += item.Quantity;
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
