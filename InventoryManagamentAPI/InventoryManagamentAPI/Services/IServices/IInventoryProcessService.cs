using InventoryManagamentAPI.Models;
using InventoryManagamentAPI.Models.DTO;

namespace InventoryManagamentAPI.Services.IServices
{
    public interface IInventoryProcessService
    {
        Task<InventoryProcessGenerateDummyReportDTO> GenerateDummyInventoryProcess(int itemId);
    }
}
