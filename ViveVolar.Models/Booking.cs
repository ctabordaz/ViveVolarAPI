﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveVolar.Models
{
    public class Booking
    {
        public string Id { get; set; }

        public string FlightId { get; set; }

        public string UserId { get; set; }

        public int Chairs { get; set; }

        public Flight Flight { get; set; }
    }
}
