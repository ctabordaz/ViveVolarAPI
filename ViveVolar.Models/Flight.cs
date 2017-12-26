using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveVolar.Models
{
    public class Flight
    {
        public DateTime Date { get; set; }

        public int Chairs { get; set; }

        public string SourceCity { get; set; }

        public string DestinationCity { get; set; }

        public int Price { get; set; }
        
        public int FlightNumber { get; set; }
    }
}
