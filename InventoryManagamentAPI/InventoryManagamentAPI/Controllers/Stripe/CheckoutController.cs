
using Humanizer;
using InventoryManagamentAPI.Data;
using InventoryManagamentAPI.Models.Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
//using Stripe.Climate; 
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StripePortfolio.Controllers
{
    //  stripe listen --forward-to https://localhost:7237/api/stripe/webhook
    //setx Stripe__SecretKey "sk_live_XXXX"
    //setx Stripe__WebhookSecret "whsec_XXXX"

    //[Authorize]

    [ApiController]
    [Route("api/checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        public CheckoutController(ApplicationDbContext dbContext, IConfiguration config
            , UserManager<IdentityUser> userManager)
        {
            _db = dbContext;
            _config = config;
            _userManager = userManager;
        }



        [HttpGet("by-session/{sessionId}")]
        public IActionResult GetOrderBySession(string sessionId)
        {
            var order = _db.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o => o.StripeCheckoutSessionId == sessionId);

            if (order == null)
                return NotFound();
    
            var dto = new
            {
                id = order.Id,
                amount = order.Amount,
                status = order.Status,
                createdAt = order.CreatedAt,
                items = order.Items.Select(i => new
                {
                    product = new
                    {
                        id = i.ProductId,
                        name = i.Product.Name
                    },
                    quantity = i.Quantity,
                    unitPrice = i.UnitPrice
                }),
 
            };

            return Ok(dto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] BuyRequest req)
        {

            if (req?.Items == null || !req.Items.Any())
                return BadRequest("No items to checkout.");

            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];

            var productIds = req.Items.Select(i => i.ProductId).ToList();

            // Load products in one query
            var products = await _db.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            var productMap = products.ToDictionary(p => p.Id);

            var lineItems = new List<SessionLineItemOptions>();
            int totalAmount = 0;

            foreach (var item in req.Items)
            {
                if (!productMap.TryGetValue(item.ProductId, out var product))
                    return BadRequest($"Product not found: {item.ProductId}");

                lineItems.Add(new SessionLineItemOptions
                {
                    Quantity = item.Quantity,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(product.Price * 100), // price in cents
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name
                        }
                    }
                });

                totalAmount += (int)(product.Price * 100 * item.Quantity);
            }

            var options = new SessionCreateOptions
            {
                Mode = "payment",
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                SuccessUrl = "https://localhost:5173/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:5173/cancel"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);
            var user = await _userManager.FindByNameAsync("admin");
            var userId = user?.Id;
            // Create order record
            var order = new InventoryManagamentAPI.Models.Stripe.Order
            {
                UserId = userId,
               // UserId = _userManager.GetUserId(User),
                Amount = totalAmount,
                Currency = "usd",
                StripeCheckoutSessionId = session.Id,
                Status = "pending",
                CreatedAt = DateTime.UtcNow
            };

            // Add order items
            foreach (var item in req.Items)
            {
                var p = productMap[item.ProductId];

                order.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = (int)(p.Price * 100) // always DB price
                });
            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return Ok(new { url = session.Url });
        }
    }
}
