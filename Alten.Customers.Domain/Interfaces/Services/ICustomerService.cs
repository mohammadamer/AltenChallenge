using Alten.Customers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Customers.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
    }
}
