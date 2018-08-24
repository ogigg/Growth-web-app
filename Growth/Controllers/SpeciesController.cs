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
    }
}