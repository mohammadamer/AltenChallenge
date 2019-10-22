using Alten.Customers.Domain.Entities;
using Alten.Customers.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alten.Customers.Models
{
    public static class CustomerDbInitializer
    {
        public static void Seed(CustomerDbContext context)
        {
            if (!context.Customers.Any())
            {
                context.AddRange
                    (
                    new Customer {  Name = "Kalles Grustransporter AB", Address = "Cementvägen 8, 111 11 Södertälje", CreatedOn = DateTime.Now },
                    new Customer {  Name = "Johans Bulk AB", Address = "Balkvägen 12, 222 22 Stockholm", CreatedOn = DateTime.Now },
                    new Customer {  Name = "Haralds Värdetransporter AB", Address = "Budgetvägen 1, 333 33 Uppsala", CreatedOn = DateTime.Now }
                    );
                var output = context.SaveChanges();
                var output1 = output;
            }
        }
    }
}
