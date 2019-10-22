using Alten.Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Customers.Infra.Data.Context
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) :
            base(options){ }

        public DbSet<Customer> Customers { get; set; }
    }
}
