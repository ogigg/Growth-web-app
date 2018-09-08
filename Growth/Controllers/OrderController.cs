using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Growth.Controllers.Resources;
using Growth.Models;
using Growth.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Growth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private GrowthDbContext _context { get; }
        private IMapper _mapper { get; }

        public OrderController(GrowthDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet("/api/orders")]
        public async Task<ICollection<OrderResource>> GetOrders()
        {
            var orders = await _context.Orders.Include(o=>o.Species).ToListAsync();
            return _mapper.Map<List<Order>, List<OrderResource>>(orders);
        }
        [HttpGet("/api/orders/{id}")]
        public Order GetOrder(int id)
        {
            return _context.Orders.Include(o => o.Species).SingleOrDefault(o => o.Id == id);
        }

        [HttpPost("/api/orders")]
        public IActionResult CreateOrder(Order order)
        {
            if (ModelState.IsValid)
                _context.Add(order);
            _context.SaveChangesAsync();

            return new JsonResult("Succes, order: " + order.Name + " created!");
        }

        [HttpPut("/api/orders/{id}")]
        public IActionResult UpdateOrder([FromBody] Order order, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            var orderInDb = _context.Orders.SingleOrDefault(o => o.Id == id);
            if (orderInDb == null)
                return NotFound();

            orderInDb.Name = order.Name;

            _context.SaveChanges();

            return new JsonResult("Succes, order with id: " + id + " is now: " + order.Name);
        }

        [HttpDelete("/api/orders/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var orderInDb = _context.Orders.SingleOrDefault(s => s.Id == id);
            if (orderInDb == null)
                return NotFound();
            _context.Remove(orderInDb);
            _context.SaveChangesAsync();

            return new JsonResult("Succes, order with id: " + id + " deleted!");
        }
    }
}