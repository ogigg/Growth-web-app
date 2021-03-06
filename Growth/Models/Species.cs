﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Models
{
    [Table("Species")]
    public class Species
    {

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        //public Order Order { get; set; }

        public int OrderId { get; set; }

        public ICollection<Feature> Features { get; set; }

        public Species()
        {
            Features = new Collection<Feature>();
        }
    }
}
