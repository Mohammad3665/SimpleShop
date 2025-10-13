using MediatR;

namespace SimpleShop.Application.Carts.Queries.GetCart
{
    public class GetCartQuery : IRequest<CartDTO>
    {
        public string UserId { get; set; }
    }
}
