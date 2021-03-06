﻿using System;
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
        private readonly int _maxFileSize = 2 * 1024 * 1024 ; //2 MB
        private readonly string[] _acceptedFileTypes = new [] {".jpg", ".jpeg",".png"};

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
            if (file == null) return BadRequest("Null File!");
            if (file.Length == 0) return BadRequest("Empty File!");
            if (file.Length > _maxFileSize) return BadRequest("File size is greater than "+ _maxFileSize/(1024*1024)+ " MB!");
            if (!_acceptedFileTypes.Any(f => f == Path.GetExtension(file.FileName).ToLower()))
                return BadRequest("Invalid file type! Try one of those: " + string.Join(",", _acceptedFileTypes)+"!");
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
        


        [HttpGet]
        public async Task<IActionResult> GetImage([FromRoute] int plantId)
        {
            var plant = await _context.Plants.Include(p=>p.Image).SingleOrDefaultAsync(p => p.Id == plantId);
            if (plant == null)
                return  new NotFoundResult();
            if (plant.Image == null)
                return new NotFoundResult();
            var image = await _context.Images.SingleOrDefaultAsync(i => i.Id == plant.Image.Id);
            if (image == null)
                return new NotFoundResult();
            return new OkObjectResult(image);
        }
    }
}