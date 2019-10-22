using Alten.Vehicles.Domain.Entities;
using Alten.Vehicles.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alten.Vehicles.Infra.Data.DbInitializer
{
    public class VehicleDbInitializer
    {
        public static void Seed(VehicleDbContext context)
        {
            if (!context.Vehicles.Any())
            {
                context.AddRange
                    (
                    new Vehicle { VehicleId = "YS2R4X20005399401", RegistrationNo = "ABC123", CustomerID = 1, CustomerName = "Kalles Grustransporter AB", LastPingTime = DateTime.Now },
                    new Vehicle { VehicleId = "VLUR4X20009093588", RegistrationNo = "DEF456", CustomerID = 1, CustomerName = "Kalles Grustransporter AB", LastPingTime = DateTime.Now },
                    new Vehicle { VehicleId = "VLUR4X20009048066", RegistrationNo = "GHI789", CustomerID = 1, CustomerName = "Kalles Grustransporter AB", LastPingTime = DateTime.Now },

                    new Vehicle { VehicleId = "YS2R4X20005388011", RegistrationNo = "JKL012", CustomerID = 2, CustomerName = "johans-bulk-ab", LastPingTime = DateTime.Now },
                    new Vehicle { VehicleId = "YS2R4X20005387949", RegistrationNo = "MNO345", CustomerID = 2, CustomerName = "johans-bulk-ab", LastPingTime = DateTime.Now },

                    new Vehicle { VehicleId = "VLUR4X20009048066", RegistrationNo = "PQR678", CustomerID = 3, CustomerName = "haralds-värdetransporter-ab", LastPingTime = DateTime.Now },
                    new Vehicle { VehicleId = "YS2R4X20005387055", RegistrationNo = "STU901", CustomerID = 3, CustomerName = "haralds-värdetransporter-ab", LastPingTime = DateTime.Now }
                    );

                var output = context.SaveChanges();
                var output1 = output;
            }
        }
    }
}
