using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Growth.Controllers.Resources;
using Growth.Models;
using Growth.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Growth.Controllers
{
    [Route("api/plants/{plantId}/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly GrowthDbContext _context;
        private readonly IMapper _mapper;

        public ImagesController(IHostingEnvironment host, GrowthDbContext context, IMapper mapper)
        {
            Host = host;
            _mapper = mapper;
            _context = context;
        }

        public IHostingEnvironment Host { get; }

        [HttpPost]
        public async Task<IActionResult> Upload(int plantId, IFormFile file)
        {
            //var plant = await _context.Plants.FindAsync(plantId);
            var plant = await _context.Plants.Include(p=>p.Image).SingleOrDefaultAsync(p => p.Id == plantId);
            //return Ok(_mapper.Map<Plant, PlantResource>(plant));

            if (plant == null)
                return NotFound("Plant not found!");
            if (plant.Image != null)
                return BadRequest("This plant has already an image!");

            var uploadFolderPath = Path.Combine(Host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var image = new Image { FileName = fileName };
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            var imageInDb = _context.Images.SingleOrDefault(i => i.FileName == fileName);

            _context.Plants.SingleOrDefault(p => p.Id == plantId).Image = imageInDb;
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<Image, ImageResource>(image));


        }
    }
}