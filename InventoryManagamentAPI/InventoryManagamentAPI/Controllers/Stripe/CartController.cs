
using InventoryManagamentAPI.Data;
using InventoryManagamentAPI.Models.Stripe;
using InventoryManagamentAPI.Models.Stripe.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 

namespace StripePortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        public CartController(ApplicationDbContext dbContext, IConfiguration config
            , UserManager<IdentityUser> userManager)
        {
            _db = dbContext;
            _config = config;
            _userManager = userManager;
        }
        [Authorize] 
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("updateCart")]
        public async Task<IActionResult> UpdateCart(UpdateCartResponseDTO updateCartResponseDTO )
        {
            var user = await _userManager.FindByNameAsync("admin");
            var userId = user?.Id;
            // var userId = _userManager.GetUserId(User);

            var cart = _db.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
                return Ok( ); // empty cart

            _db.CartItems.RemoveRange(cart.Items);

            foreach (var item in updateCartResponseDTO.Items)
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            await _db.SaveChangesAsync();
           

            return Ok( );
        }















        //  [Authorize]
        [HttpGet("my-cart")]
        public async Task< IActionResult> GetMyCart()
        {
            var user = await _userManager.FindByNameAsync("admin");
            var userId = user?.Id;
            // var userId = _userManager.GetUserId(User);

            var cart = _db.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
                return Ok(new CartDTO()); // empty cart

            // Map to DTO
            var cartDto = new CartDTO
            {
                Id = cart.Id,
                Items = cart.Items.Select(i => new CartItemDTO
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    UnitPrice = i.Product.Price,
                    Quantity = i.Quantity
                }).ToList()
            };

            return Ok(cartDto);
        }
       // [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            // var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByNameAsync("admin");
            var userId = user?.Id;
            if (request.Quantity <= 0)
                return BadRequest("Quantity must be at least 1");

            // Find user's cart or create new
            var cart = await _db.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
                _db.Carts.Add(cart);
            }

            // Check if item already exists in cart
            var existing = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

            if (existing != null)
            {
                existing.Quantity += request.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                });
            }

            await _db.SaveChangesAsync();
            return Ok(new { message = "Added to cart" });
        }

        //[Authorize]
        //[HttpDelete("api/cart/remove/{productId}")]
        //public async Task<IActionResult> RemoveItem(int productId)
        //{
        //    var userId = _userManager.GetUserId(User);

        //    var cart = await _db.Carts
        //        .Include(c => c.Items)
        //        .FirstOrDefaultAsync(c => c.UserId == userId);

        //    if (cart == null)
        //        return NotFound("Cart not found");

        //    var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        //    if (item == null)
        //        return NotFound("Item not found");

        //    _db.CartItems.Remove(item);
        //    await _db.SaveChangesAsync();

        //    return Ok(new { message = "Removed" });
        //}
        //[Authorize]
        //[HttpDelete("api/cart/clear")]
        //public async Task<IActionResult> ClearCart()
        //{
        //    var userId = _userManager.GetUserId(User);

        //    var cart = await _db.Carts
        //        .Include(c => c.Items)
        //        .FirstOrDefaultAsync(c => c.UserId == userId);

        //    if (cart == null)
        //        return Ok(); // nothing to clear

        //    _db.CartItems.RemoveRange(cart.Items);

        //    await _db.SaveChangesAsync();

        //    return Ok(new { message = "Cart cleared" });
        //}


        //[Authorize]
        //[HttpPut("api/cart/update")]
        //public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartRequest request)
        //{
        //    var userId = _userManager.GetUserId(User);

        //    if (request.Quantity <= 0)
        //        return BadRequest("Quantity must be at least 1");

        //    var cart = await _db.Carts
        //        .Include(c => c.Items)
        //        .FirstOrDefaultAsync(c => c.UserId == userId);

        //    if (cart == null)
        //        return NotFound("Cart not found");

        //    var item = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

        //    if (item == null)
        //        return NotFound("Item not found");

        //    item.Quantity = request.Quantity;

        //    await _db.SaveChangesAsync();

        //    return Ok(new { message = "Quantity updated" });
        //}
    }
}
