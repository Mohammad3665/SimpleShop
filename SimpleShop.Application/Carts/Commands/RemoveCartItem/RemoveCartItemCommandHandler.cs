using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Carts.Commands.RemoveCartItem
{
    public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public RemoveCartItemCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Unit> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _context.CartItems
                .Include(c => c.Cart)
                .FirstOrDefaultAsync(i => i.Id == request.CartItemId && i.Cart.UserId == request.UserId, cancellationToken);

            if (cartItem == null) return Unit.Value;

            _context.CartItems.Remove(cartItem);
            cartItem.Cart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
