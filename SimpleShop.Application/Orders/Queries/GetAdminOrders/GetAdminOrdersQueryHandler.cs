using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
namespace SimpleShop.Application.Orders.Queries.GetAdminOrders
{
    public class GetAdminOrdersQueryHandler : IRequestHandler<GetAdminOrdersQuery, List<AdminOrderDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetAdminOrdersQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<AdminOrderDTO>> Handle(GetAdminOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .AsNoTracking()
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new AdminOrderDTO
                {
                    OrderDate = o.OrderDate,
                    OrderId = o.Id,
                    Status = o.Status.ToString(),
                    TotalPrice = o.TotalPrice,
                    UserId = o.UserId
                }).ToListAsync(cancellationToken);


        }
    }
}
