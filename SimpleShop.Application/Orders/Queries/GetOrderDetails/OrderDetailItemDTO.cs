using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Orders.Queries.GetOrderDetails
{
    public class OrderDetailItemDTO
    {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public decimal TotalItemPrice => UnitPrice * Quantity;
    }
}
