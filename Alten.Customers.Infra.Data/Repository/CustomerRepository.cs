using Alten.Customers.Domain.Entities;
using Alten.Customers.Domain.Interfaces.Repository;
using Alten.Customers.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Customers.Infra.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext context)
        {
            _db = context;
            _dbSet = _db.Set<Customer>();
        }

        private readonly CustomerDbContext _db;
        private readonly DbSet<Customer> _dbSet;

        public virtual IEnumerable<Customer> GetAll()
        {
            return _dbSet;
        }
    }
}
