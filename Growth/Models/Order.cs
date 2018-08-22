using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Plant> Plants { get; set; }

        public Order()
        {
            Plants = new Collection<Plant>();
        }
    }
}
