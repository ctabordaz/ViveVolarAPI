﻿using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveVolar.Entities
{
    public class BookingEntity : TableEntity
    {
        public string FlightId { get; set; }
        public string UserId { get; set; }
        public int Chairs { get; set; }

        public BookingEntity()
        {
            this.PartitionKey = "Booking";
            this.RowKey = (long.MaxValue - DateTime.UtcNow.Ticks).ToString();
        }
    }
}
