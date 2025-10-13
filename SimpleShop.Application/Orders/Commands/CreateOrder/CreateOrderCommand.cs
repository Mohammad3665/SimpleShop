using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string ShippingAddress { get; set; }
        public string PostalCode { get; set; }


    }
}
