using InventoryManagamentAPI.Data;
using InventoryManagamentAPI.Models;
using InventoryManagamentAPI.Models.DTO;
using InventoryManagamentAPI.Services.IServices;

namespace InventoryManagamentAPI.Services
{
    public class InventoryProcessService:IInventoryProcessService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Guid _guid;
        private readonly Random _random;
        public InventoryProcessService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _guid = Guid.NewGuid();
            _random = new Random();
        }
        public async Task<InventoryProcessGenerateDummyReportDTO> GenerateDummyInventoryProcess(int itemId)
        {
            var item=await _dbContext.Items.FindAsync(  itemId);
            var inventoryProcess = new InventoryProcess
            {
                Name = $"Dummy Inventory Process Generation ({_guid})",
                Created = DateTime.Now,
            };
            var temporaryQuantity = item.Quantity;
            for (int i = 1; i <= 10; i++)
            {
                var detail = new InventoryProcessDetail
                {
                    ProcessType=_random.Next(2)==1?"Stock In":"Stock Out",
                     Name = $"Dummy Inventory Process Generation ({_guid}) #{i}",
                    Item = item,
                    ProcessedQuantity = _random.Next(1, 101),
                    };
                 
                detail.TotalPrice = detail.ProcessedQuantity * item.Price;

                inventoryProcess.InventoryProcessDetails.Add(detail);
                temporaryQuantity = item.Quantity+( detail.ProcessedQuantity*(detail.ProcessType == "Stock In" ?1: -1));
            }

            inventoryProcess.TotalStockInPrice =
                inventoryProcess.InventoryProcessDetails.Where(x=>x.ProcessType=="Stock In").Sum(x => x.TotalPrice);
            inventoryProcess.TotalStockOutPrice =
                inventoryProcess.InventoryProcessDetails.Where(x=>x.ProcessType=="Stock Out").Sum(x => x.TotalPrice);

            await _dbContext.InventoryProcesses.AddAsync(inventoryProcess);
            item.Quantity = temporaryQuantity;
            await _dbContext.SaveChangesAsync();

            return new InventoryProcessGenerateDummyReportDTO
            {
                InventoryProcess = inventoryProcess,
                InventoryProcessDetails = inventoryProcess.InventoryProcessDetails,
                
            };
        }
        //public async Task<InventoryProcessGenerateDummyReportDTO> GenerateDummyInventoryProcess(Item item)
        //{
        //    var temporaryInventoryProcessDetails = new List<InventoryProcessDetail>();
        //    var inventoryProcess = new InventoryProcess()
        //    {
        //        Name = $"Dummy Inventory Process Generation ({_guid})",
        //        Created=DateTime.Now,
        //    };
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        var inventoryProcessDetail = new InventoryProcessDetail()
        //        {
        //            Name=$"Dummy Inventory Process Generation ({_guid}) #{i}",
        //            Item = item,
        //            ProcessedQuantity = _random.Next(1,101),
        //        };
        //        inventoryProcessDetail.TotalPrice=inventoryProcessDetail.ProcessedQuantity*item.Price;
        //        temporaryInventoryProcessDetails.Add(inventoryProcessDetail);
        //        await _dbContext.InventoryProcessDetails.AddAsync(inventoryProcessDetail);
        //    }
        //    inventoryProcess.InventoryProcessDetails = temporaryInventoryProcessDetails;
        //    inventoryProcess.TotalProcessedPrice= inventoryProcess.InventoryProcessDetails.Sum(x => x.TotalPrice);
        //    await _dbContext.InventoryProcesses.AddAsync(inventoryProcess);

        //    await _dbContext.SaveChangesAsync();
        //    return new InventoryProcessGenerateDummyReportDTO
        //    {
        //        InventoryProcess = inventoryProcess,
        //        InventoryProcessDetails = temporaryInventoryProcessDetails
        //    };
        //}
    }
}
