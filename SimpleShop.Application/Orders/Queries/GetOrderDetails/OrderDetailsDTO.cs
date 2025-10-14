using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Orders.Queries.GetOrderDetails
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
        public string PostalCode { get; set; }
        public List<OrderDetailItemDTO> Items { get; set; }
    }
}
