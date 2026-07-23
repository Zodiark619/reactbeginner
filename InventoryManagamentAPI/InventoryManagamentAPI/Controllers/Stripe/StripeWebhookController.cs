using InventoryManagamentAPI.Data;
using InventoryManagamentAPI.Services.Stripe;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace InventoryManagamentAPI.Controllers.Stripe
{
    [Route("api/stripe/webhook")]
    [ApiController]
    public class StripeWebhookController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        private readonly IRewardService _rewardService;

        public StripeWebhookController(ApplicationDbContext dbContext, IConfiguration config, IRewardService rewardService)
        {
            _db = dbContext;
            _config = config;
            _rewardService = rewardService;
        }
        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            var signature = Request.Headers["Stripe-Signature"];

            Event stripeEvent;

            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    signature,
                    _config["Stripe:WebhookSecret"]
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }

            switch (stripeEvent.Type)
            {
                case "checkout.session.completed":
                    // var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    var session = stripeEvent.Data.Object as Session;
                    Console.WriteLine(session);
                    var order = _db.Orders
                          .Include(o => o.Items)
                          .ThenInclude(x => x.Product)

                          .FirstOrDefault(o => o.StripeCheckoutSessionId == session.Id);

                    if (order == null)
                        break;

                    // Update order status
                    order.Status = "paid";
                    order.StripePaymentIntentId = session.PaymentIntentId;
                    order.UpdatedAt = DateTime.UtcNow;

                    
                    foreach (var item in order.Items)
                    {


                        for (int i = 0; i < item.Quantity; i++) // loop per purchased pack
                        {
                           await _rewardService.SendContentToInventory(item.Product, order.UserId, order.Id);
                        }
                    }
                    await _db.SaveChangesAsync();
                    break;
            }

            return Ok();
        }
    }
}
