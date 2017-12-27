using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace ViveVolar.Entities
{
    public class FlightEntity: TableEntity
    {
        public DateTime Date { get; set; }

        public int Chairs { get; set; }

        public string SourceCity { get; set; }

        public string DestinationCity { get; set; }

        public int Price { get; set; }

        public string UserId { get; set; }

        public FlightEntity()
        {
          this.PartitionKey = "Flight";
        }

        public FlightEntity(string FlightNumber) : base("Flight", FlightNumber) { }
    }
}
