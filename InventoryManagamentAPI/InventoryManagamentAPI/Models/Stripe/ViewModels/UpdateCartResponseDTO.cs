namespace InventoryManagamentAPI.Models.Stripe.ViewModels
{
    public class UpdateCartResponseDTO
    {
        public List<UpdateCartItemResponseDTO> Items { get; set; } = new();
    }
    public class UpdateCartItemResponseDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
