using Alten.Vehicles.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Vehicles.Infra.Data.Context
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) 
            : base(options){}
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehiclePing> VehiclePings { get; set; }
    }
}
