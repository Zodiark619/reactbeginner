namespace InventoryManagamentAPI.Models.Stripe
{
    public class UserInventory
    {

        public int Id { get; set; }
        public string UserId { get; set; }         // links to your Users table
        public int ProductId { get; set; }         // links to Products table
        public int Quantity { get; set; }          // useful for digital or physical products
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
