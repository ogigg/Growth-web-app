using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Growth.Controllers.Resources;

namespace Growth.Models
{
    public class QueryResponse
    {
        public IEnumerable<PlantResource> Plants { get; set; }
        public int TotalCount { get; set; }

    }
}
