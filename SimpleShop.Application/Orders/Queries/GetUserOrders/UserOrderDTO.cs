
namespace SimpleShop.Application.Orders.Queries.GetUserOrders
{
    public class UserOrderDTO 
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public int TotalItems { get; set; }
    }
}
