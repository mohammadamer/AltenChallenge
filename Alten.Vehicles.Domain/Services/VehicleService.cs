using Alten.Vehicles.Domain.DataModels;
using Alten.Vehicles.Domain.Entities;
using Alten.Vehicles.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<VehicleViewModel> GetByCustomerAndStatus(int? customerId, bool? isConnected)
        {
            var _vehicles = _repository.GetByCustomerAndStatus(customerId, isConnected);
            var _vehiclesResult = _vehicles.Select(x => new VehicleViewModel
            {
                ID = x.ID,
                CustomerID = x.CustomerID,
                CustomerName = x.CustomerName,
                VehicleId = x.VehicleId,
                LastPingTime = x.LastPingTime,
                RegistrationNo = x.RegistrationNo,
                Isconnected = (x.LastPingTime.HasValue && x.LastPingTime >= DateTime.Now.AddMinutes(-1))
            });
            return _vehiclesResult;
        }

        public IEnumerable<VehicleViewModel> GetAll()
        {
            var _vehicles = _repository.GetAll();
            var _vehiclesResult = _vehicles.Select(x => new VehicleViewModel
            {
                ID = x.ID,
                CustomerID = x.CustomerID,
                CustomerName = x.CustomerName,
                VehicleId = x.VehicleId,
                LastPingTime = x.LastPingTime,
                RegistrationNo = x.RegistrationNo,
                Isconnected = (x.LastPingTime.HasValue && x.LastPingTime >= DateTime.Now.AddMinutes(-1))
            });
            return _vehiclesResult;
        }

        public void Update(Vehicle vehicle)
        {
            _repository.Update(vehicle);
            _repository.SaveChanges();
        }
    }
}
