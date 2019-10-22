using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Alten.Simulation.Models.Models
{
    [DataContract]
    public class MessageModel<T>
    {
        [DataMember(Name = "IsSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "ReturnMessage")]
        public string ReturnMessage { get; set; }

        [DataMember(Name = "Data")]
        public T Data { get; set; }
    }
}
