namespace InventoryManagamentAPI.Models.Stripe.ViewModels
{
    public class HistoryDetailsDTO
    {
        // Order info
        public int OrderId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }

        // List of cards for this order
        public List<HistoryDetailsCardDTO> Cards { get; set; } = new();
    }
    public class HistoryDetailsCardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Element { get; set; }
        public string Rarity { get; set; }
        public string Subtype { get; set; }
        public string Cardtype { get; set; }
        public string Set { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }

    }
}
