using Alten.Customers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Customers.Domain.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
    }
}
