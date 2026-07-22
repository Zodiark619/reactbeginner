namespace InventoryManagamentAPI.Models.Stripe.ViewModels
{
    public class CardInventoryIndexDTO
    {
        public int CardId { get; set; }
        public string Name { get; set; }
        public string Element { get; set; }
        public string Rarity { get; set; }
        public string Subtype { get; set; }
        public string Cardtype { get; set; }
        public string Set { get; set; }
        public int TotalQuantity { get; set; }
        public string ImageUrl { get; set; }
    }
}
