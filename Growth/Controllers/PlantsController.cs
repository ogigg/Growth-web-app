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
    public class PlantsController : ControllerBase
    {
        private GrowthDbContext _context { get; }
        private IMapper _mapper { get; }


        public PlantsController(GrowthDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet("/api/plants")]
        public async Task<ICollection<PlantResource>> GetPlants()
        {
            var plants = await _context.Plants.Include(p=>p.Features).ToListAsync();
            return _mapper.Map<List<Plant>, List<PlantResource>>(plants);
        }
    }
}