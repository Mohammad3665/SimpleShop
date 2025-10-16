using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Dashboard.Queries.GetDashboardStatus
{
    public class DashboardStatusDTO
    {
        public int TotalUsers { get; set; }
        public int TotalProduct { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public int PendingOrdersCount { get; set; }
    }
}
