using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;

namespace SimpleShop.Application.Carts.Queries.GetCart
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDTO>
    {
        private readonly IApplicationDbContext _context;
        public GetCartQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<CartDTO> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts
                .AsNoTracking()
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);
            if (cart == null)
            {
                return new CartDTO();
            }
            var cartDTO = new CartDTO
            {
                CartId = cart.Id,
                Items = cart.Items.Select(i => new CartItemDTO
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    ProductImageName = i.Product.ImageName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
            return cartDTO;
        }
    }
}
