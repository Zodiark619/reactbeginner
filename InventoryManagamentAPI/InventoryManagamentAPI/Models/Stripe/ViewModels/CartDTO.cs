namespace InventoryManagamentAPI.Models.Stripe.ViewModels
{
    public class CartDTO
    {
        public int Id { get; set; }
        public List<CartItemDTO> Items { get; set; } = new();
    }
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
