using SimpleShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } //Identity
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public Decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        //Address details
        public string ShippingAddress { get; set; }
        public string PostalCode { get; set; }

        //Navigation porperties
        public ICollection<OrderItem> Items = new List<OrderItem>();


    }
}
