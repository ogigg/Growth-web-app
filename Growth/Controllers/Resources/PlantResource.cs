using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Growth.Models;

namespace Growth.Controllers.Resources
{
    public class PlantResource
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int SpeciesId { get; set; }

        public string SpeciesName { get; set; }

        public int OrderId { get; set; }

        public string OrderName { get; set; }

        public int ImageId { get; set; }

        public string Description { get; set; }

        public ICollection<int> Features { get; set; }

        


        public PlantResource()
        {
            Features = new Collection<int>();
        }
        
    }
}
