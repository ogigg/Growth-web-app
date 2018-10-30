using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Growth.Controllers.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Growth.Models;
using Growth.Persistence;



namespace Growth.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ApiController 
    {
        private readonly GrowthDbContext _context;
        private readonly IMapper _mapper;
        public PlantsController(GrowthDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Plants
        [Authorize]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public QueryResponse GetPlants([FromQuery] PlantQuery plantQuery)
        {
            var plants = _context.Plants.Include(p => p.Features)
                .ThenInclude(f => f.Feature).Include(p => p.Species).Include(p => p.Image).Include(p => p.Order)
                .ToList().Select(_mapper.Map<Plant, PlantResource>);
            var totalCount = plants.Count();

            if (plantQuery.SearchFor != null)
            {
                plants = plants.Where(p => p.Name.ToLower().Contains(plantQuery.SearchFor.ToLower()) ||
                    p.SpeciesName.ToLower().Contains(plantQuery.SearchFor.ToLower()) ||
                    p.OrderName.ToLower().Contains(plantQuery.SearchFor.ToLower()));
            }

            if (plantQuery.SortBy == "order")
            {
                if (plantQuery.IsAscending)
                    plants = plants.OrderBy(p => p.OrderName);
                else
                    plants = plants.OrderByDescending(p => p.OrderName);

            }
            if (plantQuery.SortBy == "species")
            {
                if (plantQuery.IsAscending)
                    plants = plants.OrderBy(p => p.SpeciesName);
                else
                    plants = plants.OrderByDescending(p => p.SpeciesName);
            }
            if (plantQuery.SortBy == "id")
            {
                if (plantQuery.IsAscending)
                    plants = plants.OrderBy(p => p.Id);
                else
                    plants = plants.OrderByDescending(p => p.Id);
            }
            if (plantQuery.SortBy == "name")
            {
                if (plantQuery.IsAscending)
                    plants = plants.OrderBy(p => p.Name);
                else
                    plants = plants.OrderByDescending(p => p.Name);
            }
            if (plantQuery.Page != 0 && plantQuery.PageSize != 0)
                plants = plants.Skip((plantQuery.Page - 1) * plantQuery.PageSize).Take(plantQuery.PageSize);
            
            var queryResponse = new QueryResponse();
            queryResponse.Plants = plants;
            queryResponse.TotalCount = totalCount;
            return queryResponse;

        }



        // GET: api/Plants/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<IActionResult> GetPlant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
 
            var plant = _mapper.Map<Plant, PlantResource>(_context.Plants.Include(p => p.Features)
                .ThenInclude(f => f.Feature).Include(p => p.Species).Include(p => p.Image).Include(p => p.Order).SingleOrDefault(p => p.Id == id));

            if (plant == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(plant);
        }

        // PUT: api/Plants/5
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<IActionResult> PutPlant([FromRoute] int id, [Microsoft.AspNetCore.Mvc.FromBody] PlantResource plantResource)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();


            var plant = await _context.Plants.Include(p => p.Features).SingleOrDefaultAsync(p => p.Id == id);
            _mapper.Map<PlantResource, Plant>(plantResource, plant);
            await _context.SaveChangesAsync();

            //var result = _mapper.Map<Plant, PlantResource>(plant);
            return new OkObjectResult(plantResource);
        }

        // POST: api/Plants
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IActionResult> PostPlant([Microsoft.AspNetCore.Mvc.FromBody] PlantResource plantResource)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            var plant = _mapper.Map<PlantResource, Plant>(plantResource);
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<Plant, PlantResource>(plant);

            return new OkObjectResult(result);
        }

        // DELETE: api/Plants/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }

            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return new NotFoundResult();
            }

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            return new OkObjectResult(plant);
        }

        private bool PlantExists(int id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}