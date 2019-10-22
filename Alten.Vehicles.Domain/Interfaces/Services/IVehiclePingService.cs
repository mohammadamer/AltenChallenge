using Alten.Vehicles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Vehicles.Domain.Interfaces.Services
{
    public interface IVehiclePingService
    {
        void Add(VehiclePing vehiclePing);
        IEnumerable<VehiclePing> GetAll();
        VehiclePing GetById(int id);
        void Update(VehiclePing vehiclePing);
        void Remove(int id);
    }
}
