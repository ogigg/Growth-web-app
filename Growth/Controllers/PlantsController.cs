using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Growth.Controllers.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Growth.Models;
using Growth.Persistence;

namespace Growth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly GrowthDbContext _context;
        private readonly IMapper _mapper;
        public PlantsController(GrowthDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Plants
        [HttpGet]
        public IEnumerable<PlantResource> GetPlants()
        {
            var plants = _context.Plants.Include(p => p.Features)
                .ThenInclude(f => f.Feature).Include(p => p.Species).Include(p => p.Image).Include(p=>p.Order)
                .ToList().Select(_mapper.Map<Plant, PlantResource>);
            return plants;
        }

        // GET: api/Plants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var plant = _mapper.Map<Plant, PlantResource>(_context.Plants.Include(p => p.Features).SingleOrDefault(p => p.Id == id));

            if (plant == null)
            {
                return NotFound();
            }

            return Ok(plant);
        }

        // PUT: api/Plants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlant([FromRoute] int id, [FromBody] PlantResource plantResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var plant = await _context.Plants.Include(p => p.Features).SingleOrDefaultAsync(p => p.Id == id);
            _mapper.Map<PlantResource, Plant>(plantResource, plant);
            await _context.SaveChangesAsync();

            //var result = _mapper.Map<Plant, PlantResource>(plant);
            return Ok(plantResource);
        }

        // POST: api/Plants
        [HttpPost]
        public async Task<IActionResult> PostPlant([FromBody] PlantResource plantResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var plant = _mapper.Map<PlantResource, Plant>(plantResource);
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<Plant, PlantResource>(plant);

            return Ok(result);
        }

        // DELETE: api/Plants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            return Ok(plant);
        }

        private bool PlantExists(int id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}