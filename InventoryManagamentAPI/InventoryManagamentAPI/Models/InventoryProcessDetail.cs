namespace InventoryManagamentAPI.Models
{
    public class InventoryProcessDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProcessType { get; set; } = string.Empty;
        public int InventoryProcessId { get; set; }
        public InventoryProcess InventoryProcess { get; set; } = null!;
        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;
        public int ProcessedQuantity {  get; set; }
        public decimal TotalPrice {  get; set; }
    }
}
