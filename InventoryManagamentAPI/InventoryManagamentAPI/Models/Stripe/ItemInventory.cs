using Microsoft.AspNetCore.Identity;

namespace InventoryManagamentAPI.Models.Stripe
{
    public class ItemInventory
    {
        public int Id { get; set; }

        public IdentityUser User { get; set; }
        public string UserId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public int Quantity { get; set; }
        public Order Order { get; set; }
        public int? OrderId { get; set; }  // <-- add this
    }
}
