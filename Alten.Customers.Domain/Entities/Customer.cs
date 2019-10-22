using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Customers.Domain.Entities
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public String Address { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
