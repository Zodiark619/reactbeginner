namespace InventoryManagamentAPI.Models.Stripe
{
    public class BuyRequest
    {
        public List<BuyItem> Items { get; set; }
    }

    public class BuyItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
