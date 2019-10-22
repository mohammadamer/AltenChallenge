using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Alten.Simulation.Models.Models
{
    [DataContract]
    public class VehiclePingModel
    {
        
        
        [DataMember(Name = "VehicleId")]
        public int VehicleID { get; set; }
        [DataMember(Name = "PingDate")]
        public DateTime PingDate { get; set; }
    }
}
