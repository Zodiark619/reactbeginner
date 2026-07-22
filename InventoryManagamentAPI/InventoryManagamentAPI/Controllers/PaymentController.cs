using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace InventoryManagamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost("checkout")]
        public ActionResult Checkout()
        {
            var options = new SessionCreateOptions
            {
                Mode = "payment",
                SuccessUrl = "http://localhost:5173/success",
                CancelUrl = "http://localhost:5173/cancel",

                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Quantity = 1,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = 5000,

                        ProductData =
                            new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Inventory Subscription"
                            }
                    }
                }
            }
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Ok(session.Url);
        }
    }
}
