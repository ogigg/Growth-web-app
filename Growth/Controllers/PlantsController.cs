using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Growth.Models;
using Growth.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Growth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        public GrowthDbContext Context { get; }

        public PlantsController(GrowthDbContext context)
        {
            Context = context;
        }



        [HttpGet("/api/plants")]
        public async Task<ICollection<Plant>> GetPlants()
        {
            return await Context.Plants.ToListAsync();
        }
    }
}