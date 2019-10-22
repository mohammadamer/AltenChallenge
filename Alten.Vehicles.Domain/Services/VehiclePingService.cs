using Alten.Vehicles.Domain.Entities;
using Alten.Vehicles.Domain.Interfaces.Repository;
using Alten.Vehicles.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Vehicles.Domain.Services
{
    public class VehiclePingService : IVehiclePingService
    {
        private readonly IVehiclePingRepository _repository;
        public VehiclePingService(IVehiclePingRepository repository)
        {
            _repository = repository;
        }

        public void Add(VehiclePing vehiclePing)
        {
            _repository.Add(vehiclePing);
            _repository.SaveChanges();
        }

        public IEnumerable<VehiclePing> GetAll()
        {
            return _repository.GetAll();
        }

        public VehiclePing GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(VehiclePing vehiclePing)
        {
            _repository.Update(vehiclePing);
            _repository.SaveChanges();
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
            _repository.SaveChanges();
        }

    }
}
