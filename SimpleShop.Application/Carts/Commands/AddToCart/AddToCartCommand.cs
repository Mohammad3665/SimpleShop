using MediatR;

namespace SimpleShop.Application.Carts.Commands.AddToCart
{
    public class AddToCartCommand : IRequest<Unit>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public string UserId { get; set; }
    }
}
