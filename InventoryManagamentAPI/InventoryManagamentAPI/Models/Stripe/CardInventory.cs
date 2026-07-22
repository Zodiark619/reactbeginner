using Microsoft.AspNetCore.Identity;

namespace InventoryManagamentAPI.Models.Stripe
{
    public class CardInventory
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int CardId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public IdentityUser User { get; set; }
        public Card Card { get; set; }
        public int Quantity { get; set; }
        public int? OrderId { get; set; }  // <-- add this
        public Order Order { get; set; }
    }
}
