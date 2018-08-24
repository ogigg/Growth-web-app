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
    public class FeatureController : ControllerBase
    {

        private GrowthDbContext _context { get; }
        private IMapper _mapper { get; }

        public FeatureController(GrowthDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet("/api/features")]
        public async Task<ICollection<FeatureResource>> GetFeatures()
        {
            var feature = await _context.Features.ToListAsync();
            return _mapper.Map<List<Feature>, List<FeatureResource>>(feature);
        }

    }
}