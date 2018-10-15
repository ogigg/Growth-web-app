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
            CreateMap<Order, OrderResource>();
            CreateMap<Species, SpeciesResource>();
            CreateMap<Feature, FeatureResource > ();
            CreateMap<Plant, PlantResource> ()
                .ForMember( pr => pr.Features, opt => opt.MapFrom(p => p.Features.Select(pf => pf.FeatureId)));

            //API Resource to Domain Model
            CreateMap<PlantResource, Plant>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.Features, opt => opt.Ignore()).AfterMap((pr, p) =>
                {
                    var removedFeatures = new List<PlantFeature>();
                    foreach (var f in p.Features)
                    {
                        if (!pr.Features.Contains(f.FeatureId))
                            removedFeatures.Add(f);
                    }

                    foreach (var f in removedFeatures)
                    {
                        p.Features.Remove(f);
                    }

                    foreach (var id in pr.Features)
                    {
                        if(!p.Features.Any(f=>f.FeatureId==id))
                            p.Features.Add(new PlantFeature{FeatureId = id});
                    }
                });
        }
    }
}
