using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITransactionService _transactionService;
        public CreateOrderCommandHandler(IApplicationDbContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .Include(p => p.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

            if (cart == null || !cart.Items.Any())
            {
                throw new Exception("Cart not found or empty.");
            }
            using IDbContextTransaction transaction = await _transactionService.BeginTransactionAsync(cancellationToken);

            try
            {
                decimal totalPrice = 0;
                var orderItems = new List<OrderItem>();
                foreach (var cartItem in cart.Items)
                {
                    var product = cartItem.Product;
                    if (product.Stock < cartItem.Quantity)
                    {
                        throw new Exception($"The stock of product {product.Name} is only {product.Stock} numbers.");
                    }
                    product.Stock -= cartItem.Quantity;
                    orderItems.Add(new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        UnitPrice = cartItem.UnitPrice,
                    });
                    totalPrice += cartItem.Quantity * cartItem.UnitPrice;
                }
                var order = new Order
                {
                    UserId = request.UserId,
                    OrderDate = DateTime.Now,
                    TotalPrice = totalPrice,
                    PostalCode = request.PostalCode,
                    ShippingAddress = request.ShippingAddress,
                    Status = OrderStatus.Pending
                };
                foreach(var item in orderItems)
                {
                    order.Items.Add(item);
                }
                _context.Orders.Add(order);
                _context.CartItems.RemoveRange(cart.Items);
                _context.Carts.Remove(cart);

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return order.Id;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
