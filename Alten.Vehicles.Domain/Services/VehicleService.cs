using Alten.Vehicles.Domain.Entities;
using Alten.Vehicles.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Vehicles.Domain.Interfaces.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _repository;

        public VehicleService(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public void Add(Vehicle Vehicle)
        {
            _repository.Add(Vehicle);
            _repository.SaveChanges();
        }

        public Vehicle GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Vehicle GetByVidAndRegNo(string vehicleId, string registrationNo)
        {
            return _repository.GetByVidAndRegNo(vehicleId, registrationNo);
        }

        public IEnumerable<Vehicle> GetByCustomerAndStatus(int? customerId, bool? isConnected)
        {
            return _repository.GetByCustomerAndStatus(customerId, isConnected);
        }

        public IEnumerable<Vehicle> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(Vehicle vehicle)
        {
            _repository.Update(vehicle);
            _repository.SaveChanges();
        }
    }
}
