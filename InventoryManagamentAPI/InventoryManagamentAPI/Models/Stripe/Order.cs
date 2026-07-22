using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace InventoryManagamentAPI.Models.Stripe
{
    public class Order
    {
        public int Id { get; set; }

        public List<OrderItem> Items { get; set; } = new();
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; } = "usd";
        public string? StripePaymentIntentId { get; set; }
        public string StripeCheckoutSessionId { get; set; }
        // pending / paid / failed / refunded
        public string Status { get; set; } = "pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // 👇 Add this computed property
        public decimal TotalAmount => Amount / 100m;

    }
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public int UnitPrice { get; set; } // in cents
    }
}
