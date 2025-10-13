
namespace SimpleShop.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        //Navigation properties
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

    }
}
