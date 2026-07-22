using System.Text.Json.Serialization;

namespace InventoryManagamentAPI.Models.Stripe
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public List<CartItem> Items { get; set; } = new();
    }
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }
        [JsonIgnore]
        public Cart Cart { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
