namespace InventoryManagamentAPI.Models
{
    public class InventoryProcess
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public DateTime Created { get; set; }

        public List<InventoryProcessDetail> InventoryProcessDetails { get; set; } = new();



        public decimal TotalStockInPrice {  get; set; }
        public decimal TotalStockOutPrice {  get; set; }
    }
}
