using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveVolar.Models
{
    public class Search
    {
        public DateTime? InitDate {get; set;}
        public DateTime? EndDate { get; set; }
        public string SourceCity {get; set;}
        public string DestinationCity {get; set;}
        public int? Chairs {get; set;}
    }
}
