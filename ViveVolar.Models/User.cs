﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveVolar.Models
{
    public class User
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Rol { get; set; }

        public IEnumerable<Flight> Flights { get; set; }
    }
}
