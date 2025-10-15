using MediatR;
namespace SimpleShop.Application.Orders.Queries.GetAdminOrders
{
    public class GetAdminOrdersQuery : IRequest<List<AdminOrderDTO>>
    {
    }
}
