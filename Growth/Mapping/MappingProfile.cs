using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Growth.Controllers.Resources;
using Growth.Models;

namespace Growth.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderResource>();
            CreateMap<Plant, PlantResource>();
        }
    }
}
