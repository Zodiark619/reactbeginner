namespace InventoryManagamentAPI.Models.DTO
{
    public class InventoryProcessDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Created { get; set; }

        public List<InventoryProcessDetailDTO> InventoryProcessDetails { get; set; } = new();


        public int FinalQuantity { get; set; }
        public decimal TotalStockInPrice { get; set; }
        public decimal TotalStockOutPrice { get; set; }
    }
}
