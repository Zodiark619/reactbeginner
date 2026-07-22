using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StripePortfolio.Data;
using StripePortfolio.Models;
using StripePortfolio.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripePortfolio.Controllers
{
     
    public class CardInventoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CardInventoriesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: GrandArchive/CardInventories
        public async Task<IActionResult> Index()
        {
            //var userId = _userManager.GetUserId(User);
            //var applicationDbContext = _context.CardInventories.Include(c => c.Card)
            //    .Include(c => c.Order)
            //    .Include(c => c.User)
            //    .Where(o => o.UserId == userId);
            //return View(await applicationDbContext.ToListAsync());

            var userId = _userManager.GetUserId(User);
            var inventoryRaw = _context.CardInventories
    .Where(x => x.UserId == userId)
    .Include(x => x.Card)
    .ThenInclude(x => x.Elements)
    .Include(x => x.Card.Subtypes)
    .Include(x => x.Card.CardTypes)
    .Include(x => x.Card.Sets)
    .Include(x => x.Card.Rarity).ToList();


            var inventory= inventoryRaw
    .GroupBy(x => x.CardId)
    .Select(g => new CardInventoryIndexDto
    {
        CardId = g.Key,
        Name = g.First().Card.Name,
        Element = string.Join(", ", g.First().Card.Elements.Select(e => e.Name)),
        Rarity = g.First().Card.Rarity.Name,
        Subtype = string.Join(", ", g.First().Card.Subtypes.Select(s => s.Name)),
        Cardtype = string.Join(", ", g.First().Card.CardTypes.Select(t => t.Name)),
        Set = string.Join(", ", g.First().Card.Sets.Select(s => s.Name)),
        ImageUrl = g.First().Card.ImageUrl,
        TotalQuantity = g.Sum(x => x.Quantity)
    })
    .ToList();
            return View(inventory);
        }

        // GET: GrandArchive/CardInventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardInventory = await _context.CardInventories
                .Include(c => c.Card)
                .Include(c => c.Order)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardInventory == null)
            {
                return NotFound();
            }

            return View(cardInventory);
        }

        // GET: GrandArchive/CardInventories/Create
        public IActionResult Create()
        {
            ViewData["CardId"] = new SelectList(_context.Card, "Id", "Name");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: GrandArchive/CardInventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CardId,CreatedAt,Quantity,OrderId")] CardInventory cardInventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardInventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Card, "Id", "Name", cardInventory.CardId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", cardInventory.OrderId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cardInventory.UserId);
            return View(cardInventory);
        }

        // GET: GrandArchive/CardInventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardInventory = await _context.CardInventories.FindAsync(id);
            if (cardInventory == null)
            {
                return NotFound();
            }
            ViewData["CardId"] = new SelectList(_context.Card, "Id", "Name", cardInventory.CardId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", cardInventory.OrderId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cardInventory.UserId);
            return View(cardInventory);
        }

        // POST: GrandArchive/CardInventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CardId,CreatedAt,Quantity,OrderId")] CardInventory cardInventory)
        {
            if (id != cardInventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardInventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardInventoryExists(cardInventory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardId"] = new SelectList(_context.Card, "Id", "Name", cardInventory.CardId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", cardInventory.OrderId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", cardInventory.UserId);
            return View(cardInventory);
        }

        // GET: GrandArchive/CardInventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardInventory = await _context.CardInventories
                .Include(c => c.Card)
                .Include(c => c.Order)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cardInventory == null)
            {
                return NotFound();
            }

            return View(cardInventory);
        }

        // POST: GrandArchive/CardInventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardInventory = await _context.CardInventories.FindAsync(id);
            if (cardInventory != null)
            {
                _context.CardInventories.Remove(cardInventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardInventoryExists(int id)
        {
            return _context.CardInventories.Any(e => e.Id == id);
        }
    }
}
