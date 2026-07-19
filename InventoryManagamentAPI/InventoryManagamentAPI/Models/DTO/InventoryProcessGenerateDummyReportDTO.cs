namespace InventoryManagamentAPI.Models.DTO
{
    public class InventoryProcessGenerateDummyReportDTO
    {

        public InventoryProcess InventoryProcess { get; set; } = new();
        public List<InventoryProcessDetail> InventoryProcessDetails { get; set; } = new();
    }
}
