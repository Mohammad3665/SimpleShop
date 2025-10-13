
namespace SimpleShop.Application.Carts.Queries.GetCart
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();

        public decimal TotalAmount => Items.Sum(i => i.TotalItemPrice);
    }
}
