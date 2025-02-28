using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FantasyStoreAPI.Entities;
using FantasyStoreAPI.DTOs;

namespace FantasyStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly FantasyStoreDbContext _context;

        public OrdersController(FantasyStoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _context.Orders.Include(o => o.Item).ToListAsync();

            var orderDTOs = orders.Select(order => new OrderDTO
            {
                Id = order.Id,
                ItemName = order.Item.ItemName,
                Quantity = order.Quantity,
                OrderDate = (DateTime)order.OrderDate
            }).ToList();

            return Ok(orderDTOs);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderCreateDTO orderDTO)
        {
            var item = await _context.Inventories.FindAsync(orderDTO.ItemId);
            if (item == null) return NotFound("Item not found.");

            if (item.StockQuantity < orderDTO.Quantity)
                return BadRequest("Not enough stock available.");

            // Deduct stock
            item.StockQuantity -= orderDTO.Quantity;

            // Save order
            var newOrder = new Order
            {
                ItemId = orderDTO.ItemId,
                Quantity = orderDTO.Quantity,
                OrderDate = DateTime.UtcNow
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrders), new { id = newOrder.Id }, new OrderDTO
            {
                Id = newOrder.Id,
                ItemName = item.ItemName,
                Quantity = newOrder.Quantity,
                OrderDate = (DateTime)newOrder.OrderDate
            });
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
