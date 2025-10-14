using MediatR;

namespace SimpleShop.Application.Orders.Queries.GetUserOrders
{
    public class GetUserOrdersQuery : IRequest<List<UserOrderDTO>>
    {
        public string UserId { get; set; }
    }
}
