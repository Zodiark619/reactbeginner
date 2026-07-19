using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagamentAPI.Data;
using InventoryManagamentAPI.Models;
using InventoryManagamentAPI.Models.DTO;

namespace InventoryManagamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Items
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        //{
        //    return await _context.Items.ToListAsync();
        //}
        [HttpGet("all")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _context.Items
                .OrderBy(i => i.Name)
                .ToListAsync();

            return Ok(items);
        }
        [HttpGet]
        public async Task<IActionResult> GetItems(
    int page = 1,
    int pageSize = 10)
        {
            var totalCount = await _context.Items.CountAsync();

            var items = await _context.Items
                .OrderBy(i => i.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                items,
                totalCount
            });
        }
            // GET: api/Items/5
            [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, ItemUpdateDTO itemDto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }


            item.Name = itemDto.Name;
                item.Price = itemDto.Price;
            
            
                await _context.SaveChangesAsync();
            

            return NoContent();
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(ItemCreateDTO itemDto)
        {
            var item= new Item()
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
            };
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }
      

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
