using Alten.Customers.Domain.Entities;
using Alten.Customers.Domain.Interfaces.Repository;
using Alten.Customers.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Customers.Domain
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
