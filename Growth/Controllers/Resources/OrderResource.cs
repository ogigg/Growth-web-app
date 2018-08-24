using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Growth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Growth.Controllers.Resources
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SpeciesResource> Species { get; set; }

        public OrderResource()
        {
            Species = new Collection<SpeciesResource>();
        }
    }
}