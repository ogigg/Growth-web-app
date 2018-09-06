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

        [HttpPost("/api/orders")]
        public IActionResult CreateOrder(Order order)
        {

            if (ModelState.IsValid)
                _context.Add(order);
            _context.SaveChangesAsync();

            return new JsonResult("Succes, order: " + order.Name + " created!");
        }


    }
}