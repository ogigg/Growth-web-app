using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Growth.Controllers.Resources;
using Growth.Models;
using Growth.Persistence;
using System.Web.Http.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using BadRequestResult = Microsoft.AspNetCore.Mvc.BadRequestResult;
using NotFoundResult = Microsoft.AspNetCore.Mvc.NotFoundResult;
using OkResult = Microsoft.AspNetCore.Mvc.OkResult;


namespace Growth.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
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

        [Microsoft.AspNetCore.Mvc.HttpGet("/api/features/{id}")]
        public Feature GetFeature(int id)
        {
            return _context.Features.SingleOrDefault(f=>f.Id == id);
        }

        [Microsoft.AspNetCore.Mvc.HttpPut("/api/features/{id}")]
        public IActionResult UpdateFeature([Microsoft.AspNetCore.Mvc.FromBody] Feature feature, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feature.Id)
            {
                return BadRequest();
            }

            var featureInDb = _context.Features.SingleOrDefault(f => f.Id == id);
            if (featureInDb == null)
                return NotFound();

            featureInDb.Name = feature.Name;

            _context.SaveChanges();

            return new JsonResult("Succes, feature with id: " + id + " is now: " + feature.Name);
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

            return new JsonResult("Succes, feature: " + feature.Name + " created!");
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("/api/features/{id}")]
        public IActionResult DeleteFeature(int id)
        {
            var featureInDb = _context.Features.SingleOrDefault(f => f.Id == id);
            if (featureInDb == null)
                return NotFound();
            _context.Remove(featureInDb);
            _context.SaveChangesAsync();

            return new JsonResult("Succes, feature with id: "+id+" deleted!");
        }
    }

}