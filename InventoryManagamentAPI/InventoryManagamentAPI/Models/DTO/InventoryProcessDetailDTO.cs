namespace InventoryManagamentAPI.Models.DTO
{
    public class InventoryProcessDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProcessType { get; set; } = string.Empty;
        
        public string ItemName { get; set; } = string.Empty;
        public decimal ItemPrice { get; set; }
        public int ProcessedQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
