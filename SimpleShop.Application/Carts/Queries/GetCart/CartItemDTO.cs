
namespace SimpleShop.Application.Carts.Queries.GetCart
{
    public class CartItemDTO
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalItemPrice => UnitPrice * Quantity;
    }
}
