using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Growth.Controllers.Resources;
using Growth.Models;
using Growth.Persistence;
using System.Web.Http.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;


namespace Growth.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ApiController
    {

        private GrowthDbContext _context { get; }
        private IMapper _mapper { get; }

        public FeatureController(GrowthDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [Microsoft.AspNetCore.Mvc.HttpGet("/api/features/{id}")]
        public Feature GetFeature(int id)
        {
            return _context.Features.SingleOrDefault(f=>f.Id == id);
            
        }
        [Microsoft.AspNetCore.Mvc.HttpPut("/api/features/{id}")]
        public IActionResult UpdateFeature(Feature feature, int id)
        {
            if (!ModelState.IsValid)
                return new BadRequestResult();

            var featureInDb = _context.Features.SingleOrDefault(f => f.Id == id);

            if (featureInDb == null)
                return new NotFoundResult();

            _mapper.Map(feature, featureInDb);
            _context.SaveChanges();

            return new OkResult();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("/api/features")]
        public async Task<ICollection<FeatureResource>> GetFeatures()
        {
            var features = await _context.Features.ToListAsync();
            return _mapper.Map<List<Feature>, List<FeatureResource>>(features);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("/api/features")]
        public IActionResult CreateFeature( Feature feature)
        {

            if (ModelState.IsValid)
                _context.Add(feature);
            _context.SaveChangesAsync();

            return new OkObjectResult(feature.Name);
        }

    }

}