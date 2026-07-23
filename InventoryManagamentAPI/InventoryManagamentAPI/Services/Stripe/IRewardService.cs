using InventoryManagamentAPI.Models.Stripe;

namespace InventoryManagamentAPI.Services.Stripe
{
    public interface IRewardService
    {
         Task<SendToInventoryRequest> SendContentToInventory(Product product, string userId, int orderId);
    }
}
