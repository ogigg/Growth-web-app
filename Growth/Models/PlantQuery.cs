﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Models
{
    public class PlantQuery
    {
        public string SortBy { get; set; }

        public bool IsAscending { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SearchFor { get; set; }
    }
}
