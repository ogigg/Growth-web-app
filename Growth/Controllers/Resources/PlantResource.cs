﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Growth.Models;

namespace Growth.Controllers.Resources
{
    public class PlantResource
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
