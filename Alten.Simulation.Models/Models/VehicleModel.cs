using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Alten.Simulation.Models.Models
{
    [DataContract]
    public class VehicleModel
    {
        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [DataMember(Name = "VehicleId")]
        public string VehicleId { get; set; 
        }
        [DataMember(Name = "RegistrationNo")]
        public string RegistrationNo { get; set; }

        [DataMember(Name = "CustomerID")]
        public int CustomerID { get; set; }

        [DataMember(Name = "CustomerName")]
        public string CustomerName { get; set; }

        [DataMember(Name = "LastPingTime")]
        public DateTime LastPingTime { get; set; }
    }
}
