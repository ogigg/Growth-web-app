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
    public class SpeciesController : ControllerBase
    {
        private GrowthDbContext _context { get; }
        private IMapper _mapper { get; }


        public SpeciesController(GrowthDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet("/api/species")]
        public async Task<ICollection<SpeciesResource>> GetSpecies()
        {
            var species = await _context.Species.Include(s=>s.Features).ToListAsync();
            return _mapper.Map<List<Species>, List<SpeciesResource>>(species);
        }
        
        [HttpGet("/api/species/{id}")]
        public Species GetSpecies(int id)
        {
            return _context.Species.SingleOrDefault(s => s.Id == id);
        }

        [HttpPost("/api/species")]
        public IActionResult CreateSpecies(Species species)
        {
            if (ModelState.IsValid)
                _context.Add(species);
            _context.SaveChangesAsync();

            return new JsonResult("Succes, species: " + species.Name + " created!");
        }

        [HttpPut("/api/species/{id}")]
        public IActionResult UpdateSpecies([FromBody] Species species, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != species.Id)
            {
                return BadRequest();
            }

            var speciesInDb = _context.Species.SingleOrDefault(s => s.Id == id);
            if (speciesInDb == null)
                return NotFound();

            speciesInDb.Name = species.Name;

            _context.SaveChanges();

            return new JsonResult("Succes, species with id: " + id + " is now: " + species.Name);
        }

        [HttpDelete("/api/species/{id}")]
        public IActionResult DeleteSpecies(int id)
        {
            var speciesInDb = _context.Species.SingleOrDefault(s => s.Id == id);
            if (speciesInDb == null)
                return NotFound();
            _context.Remove(speciesInDb);
            _context.SaveChangesAsync();

            return new JsonResult("Succes, species with id: " + id + " deleted!");
        }

    }
}