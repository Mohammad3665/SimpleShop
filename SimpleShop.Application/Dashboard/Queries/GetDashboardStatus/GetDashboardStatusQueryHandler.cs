using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Enums;
using SimpleShop.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Dashboard.Queries.GetDashboardStatus
{
    public class GetDashboardStatusQueryHandler : IRequestHandler<GetDashboardStatusQuery, DashboardStatusDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public GetDashboardStatusQueryHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<DashboardStatusDTO> Handle(GetDashboardStatusQuery request, CancellationToken cancellationToken)
        {
            var totalUsersTask = _userManager.Users.CountAsync(cancellationToken);
            var totalProductsTask = _context.Products.CountAsync(cancellationToken);
            var totalOrdersTask = _context.Orders.CountAsync(cancellationToken);
            var totalSalesTask = _context.Orders.SumAsync(o => o.TotalPrice, cancellationToken);
            var pendingOrdersTask = _context.Orders.CountAsync(o => o.Status == OrderStatus.Pending);

            await Task.WhenAll(totalUsersTask, totalProductsTask, totalOrdersTask, totalSalesTask, pendingOrdersTask);
            return new DashboardStatusDTO
            {
                TotalUsers = totalUsersTask.Result,
                TotalProduct = totalProductsTask.Result,
                TotalOrders = totalOrdersTask.Result,
                TotalSalesAmount = totalSalesTask.Result,
                PendingOrdersCount = pendingOrdersTask.Result,
            };
        }
    }
}
