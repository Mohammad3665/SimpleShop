using MediatR;
using SimpleShop.Domain.Enums;
namespace SimpleShop.Application.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest<Unit>
    {
        public int OrderId { get; set; }
        public OrderStatus NewOrderStatus { get; set; }
    }
}
