using Alten.Vehicles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Vehicles.Domain.Interfaces.Services
{
    public interface IVehicleService
    {
        void Add(Vehicle Vehicle);
        Vehicle GetById(int id);
        Vehicle GetByVidAndRegNo(string vehicleId, string registrationNo);
        IEnumerable<Vehicle> GetByCustomerAndStatus(int? customerId, bool? isConnected);
        IEnumerable<Vehicle> GetAll();
        void Update(Vehicle vehicle);
    }
}
