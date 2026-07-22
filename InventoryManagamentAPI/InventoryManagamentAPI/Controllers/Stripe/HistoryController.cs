using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StripePortfolio.Data;
using StripePortfolio.Models.ViewModels;
using System.Security.Claims;

namespace StripePortfolio.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public HistoryController(ApplicationDbContext applicationDb, UserManager<IdentityUser> userManager)
        {
            _db = applicationDb;
            _userManager = userManager;

        }
        [Authorize] 
        public IActionResult Index(string filter )
        {
            var userId = _userManager.GetUserId(User);

            var query = _db.Orders
       .Include(o => o.Items)
       .ThenInclude(i => i.Product)
       .Where(o => o.UserId == userId);


       if (filter != null)
            {
                query=query.Where(x=>x.Status==filter);
            }
       var orders=query.OrderByDescending(x=>x.CreatedAt).ToList();
            return View(orders);
        }
        [Authorize]
        public IActionResult Details([FromRoute (Name ="id")]int orderid)
        {
            var userId = _userManager.GetUserId(User);

            var order = _db.Orders
                .Include(o => o.Items)
       .ThenInclude(i => i.Product) 

                .FirstOrDefault(x => x.Id == orderid && x.UserId == userId);
            if (order == null)
                return NotFound();

            var cards = _db.CardInventories
                .Where(x => x.UserId == userId && x.OrderId == orderid)
                .Select(x => new HistoryDetailsCardDto
                {
                    
                    Id = x.Card.Id, 
                    Name = x.Card.Name,
                    Element = string.Join(", ", x.Card.Elements.Select(x => x.Name)),
                    Rarity =   x.Card.Rarity.Name ,
                    Subtype = string.Join(", ", x.Card.Subtypes.Select(x => x.Name)),
                    Cardtype = string.Join(", ", x.Card.CardTypes.Select(x => x.Name)),
                    Set = string.Join(", ", x.Card.Sets.Select(x => x.Name)),
                    Quantity = x.Quantity,
                    ImageUrl = x.Card.ImageUrl
                }).ToList();

            var dto = new HistoryDetailsDto
            {
                OrderId = order.Id,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                TotalAmount = order.Amount,
                Cards = cards,
            };
            return View(dto);
        }


    }
}
