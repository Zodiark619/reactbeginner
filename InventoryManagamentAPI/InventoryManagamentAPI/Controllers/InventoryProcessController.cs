using InventoryManagamentAPI.Models;
using InventoryManagamentAPI.Models.DTO;
using InventoryManagamentAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryProcessController : ControllerBase
    {
        private readonly IInventoryProcessService _inventoryProcessService;

        public InventoryProcessController(IInventoryProcessService inventoryProcessService)
        {
            _inventoryProcessService = inventoryProcessService;
        }

        [HttpPost("generate-dummy/${itemId}")]
        public async Task<ActionResult<InventoryProcessGenerateDummyReportDTO>> GenerateDummy(int itemId)
        {
            

            var report = await _inventoryProcessService.GenerateDummyInventoryProcess(itemId);

            return Ok(report);
        }
    }
}
