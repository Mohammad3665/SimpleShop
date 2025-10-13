using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Application.Carts.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        public AddToCartCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { request.ProductId }, cancellationToken);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            if(product.Stock < request.Quantity)
            {
                throw new Exception($"The number of products in stock is less than the quantity you requested. Stock availability: {product.Stock}");
            }
            var cart = await _context.Carts
                .Include(p => p.Items)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = request.UserId,
                    CreatedDate = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Carts.Add(cart);
            }
            var cartItem =  cart.Items.FirstOrDefault(item => item.ProductId == request.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += request.Quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    UnitPrice = product.Price,
                    Cart = cart
                };
                cart.Items.Add(cartItem);
            }
            cart.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
