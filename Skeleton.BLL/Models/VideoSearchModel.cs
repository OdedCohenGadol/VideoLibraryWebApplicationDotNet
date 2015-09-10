using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.BLL.Models
{
    public class VideoSearchModel
    {
        public string Name { get; set; }
        public string  Director { get; set; }
        public string Genre { get; set; }
        public int? Year { get; set; }
    }
}
