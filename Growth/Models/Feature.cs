using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Models
{
    public class Feature
    {
        public Feature()
        {
            this.Plants = new HashSet<Plant>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
    }
}
