using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
namespace SimpleShop.Application.Orders.Queries.GetUserOrders
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, List<UserOrderDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetUserOrdersQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<UserOrderDTO>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(p => p.UserId == request.UserId)
                .OrderByDescending(p => p.OrderDate)
                .Select(p => new UserOrderDTO
                {
                    OrderDate = p.OrderDate,
                    OrderId = p.Id,
                    Status = p.Status.ToString(),
                    TotalItems = p.Items.Sum(s => s.Quantity),
                    TotalPrice = p.TotalPrice,
                    ProductName = p.Items.First().Product.Name,
                }).ToListAsync(cancellationToken);
        }
    }
}
