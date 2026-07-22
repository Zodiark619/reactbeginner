namespace InventoryManagamentAPI.Models.Stripe.ViewModels
{
    public class AddToCartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class UpdateCartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
