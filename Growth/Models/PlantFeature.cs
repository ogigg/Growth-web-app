using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Models
{
    [Table("PlantFeatures")]
    public class PlantFeature
    {
        public int PlantId { get; set; }
        public int FeatureId { get; set; }
        public Plant Plant { get; set; }
        public Feature Feature { get; set; }
        

    }
}
