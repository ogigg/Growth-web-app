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
            //Domain model to API Resource
            CreateMap<Image, ImageResource>();
            CreateMap<Order, OrderResource>();
            CreateMap<Species, SpeciesResource>();
            CreateMap<Feature, FeatureResource > ();
            CreateMap<Plant, PlantResource>()
                .ForMember(pr => pr.Features, opt => opt.MapFrom(p => p.Features.Select(pf => pf.FeatureId)))
                .ForMember(pr => pr.OrderId, opt => opt.MapFrom(p => p.Species.OrderId))
                .ForMember(pr => pr.OrderName, opt => opt.MapFrom(p => p.Order.Name));
            CreateMap<User, UsersResource>();

            //API Resource to Domain Model
            CreateMap<UsersResource, User>();
            CreateMap<PlantResource, Plant>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                //.ForMember(p=>p.Order, opt=>opt.MapFrom(o=>o.OrderId))
                //.ForMember(p=>p.Image, opt => opt.MapFrom(p=>p.ImageId))
                .ForMember(p => p.Features, opt => opt.Ignore()).AfterMap((pr, p) =>
                {
                    var removedFeatures = p.Features.Where(f => !pr.Features.Contains(f.FeatureId)).ToList();
                    foreach (var f in removedFeatures)
                        p.Features.Remove(f);

                    var addedFeatures = pr.Features.Where(id => p.Features.All(f => f.FeatureId != id))
                        .Select(id=>new PlantFeature{ FeatureId = id });
                    foreach (var f in addedFeatures)
                        p.Features.Add(f);
                });
        }
    }
}
