using InventoryManagamentAPI.Data;
using InventoryManagamentAPI.Models.Stripe;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagamentAPI.Services.Stripe
{
    public class RewardService:IRewardService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Random _random;
        public RewardService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _random = new Random();
        }
        public async Task AddItemsToInventory(string userId, SendToInventoryRequest request, int orderId)
        {
            foreach (var item in request.Items)
            {
                var existing =await _dbContext.ItemInventories
                    .FirstOrDefaultAsync(x => x.UserId == userId &&
                    x.ItemId == item.ItemId
                    && x.OrderId == orderId);

                if (existing != null)
                {
                    existing.Quantity+=item.Quantity;
                }
                else
                {
                   await _dbContext.ItemInventories.AddAsync(new ItemInventory
                    {
                        UserId = userId,
                        ItemId = item.ItemId,
                        Quantity = item.Quantity,
                        OrderId = orderId
                    });
                }
            }
           await _dbContext.SaveChangesAsync(); // single save

        }


        public async Task< SendToInventoryRequest> SendContentToInventory(Product product, string userId, int orderId)
        {
            var content = new SendToInventoryRequest();
            if (product.YieldContent == "CaramelComboPack")
            {
                content = CaramelComboPack();
            }
           await AddItemsToInventory(userId, content, orderId);
            return content;
        }
        private SendToInventoryRequest CaramelComboPack()
        {
            SendToInventoryRequest buyRequest = new SendToInventoryRequest
            {
                Items=new List<SendToInventoryItemRequest>
                {
                    new SendToInventoryItemRequest
                    {
                        ItemId=1,
                        Quantity=5,
                    },
                     new SendToInventoryItemRequest
                    {
                        ItemId=2,
                        Quantity=5,
                    },
                      new SendToInventoryItemRequest
                    {
                        ItemId=3,
                        Quantity=5,
                    }
                }
            };
           
            return buyRequest;
        }

    }
}
