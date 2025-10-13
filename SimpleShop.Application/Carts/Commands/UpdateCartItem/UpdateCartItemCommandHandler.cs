using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Carts.Commands.UpdateCartItem
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCartItemCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Unit> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _context.CartItems
                .Include(i => i.Product)
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.Id == request.CartItemId && i.Cart.UserId == request.UserId, cancellationToken);
            if (cartItem == null)
            {
                throw new Exception("Cart or shopping cart item not found.");
            }
            if (request.NewQuantity <= 0)
            {
                _context.CartItems.Remove(cartItem);
            }
            else
            {
                if(cartItem.Product.Stock <  request.NewQuantity)
                {
                    throw new Exception($"The stock of product {cartItem.Product.Name} is only {cartItem.Product.Stock} numbers.");
                }
                cartItem.Quantity = request.NewQuantity;
                cartItem.Cart.UpdatedAt = DateTime.UtcNow;

            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
