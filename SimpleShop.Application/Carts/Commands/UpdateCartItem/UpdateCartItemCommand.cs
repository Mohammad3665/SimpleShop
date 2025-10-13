using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Application.Carts.Commands.UpdateCartItem
{
    public class UpdateCartItemCommand : IRequest<Unit>
    {
        public int CartItemId { get; set; }
        public string UserId { get; set; }
        public int NewQuantity { get; set; }
    }
}
