﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Models
{
    public class Plant
    {
        public Plant()
        {
            this.Features = new Collection<PlantFeature>();
        }
        public int Id { get; set; }
        public int SpeciesId { get; set; }
        public Species Species { get; set; }


        public virtual ICollection<PlantFeature> Features { get; set; }
    }
}