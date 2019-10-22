using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Vehicles.Domain.DataModels
{
    public class UpdateVehicles
    {
        public int ID { get; set; }
        public string VehicleId { get; set; }
        public string RegistrationNo { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime LastPingTime { get; set; }
    }
}
