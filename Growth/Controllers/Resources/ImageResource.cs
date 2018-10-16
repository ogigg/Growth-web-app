using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Growth.Controllers.Resources
{
    public class ImageResource
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string Caption { get; set; }
    }
}
