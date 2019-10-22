using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Vehicles.Domain.Entities
{
    public class VehiclePing
    {
        public long ID { get; set; }
        public int VehicleID { get; set; }
        public DateTime PingDate { get; set; }
    }
}
