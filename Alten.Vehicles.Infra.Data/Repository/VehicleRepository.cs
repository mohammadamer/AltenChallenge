using Alten.Vehicles.Domain.Entities;
using Alten.Vehicles.Domain.Interfaces.Repository;
using Alten.Vehicles.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alten.Vehicles.Infra.Data.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleDbContext _db;
        private readonly DbSet<Vehicle> _dbSet;
        public VehicleRepository(VehicleDbContext context)
        {
            _db = context;
            _dbSet = _db.Set<Vehicle>();
        }

        public virtual void Add(Vehicle vehicle)
        {
            _dbSet.Add(vehicle);
        }

        public Vehicle GetById(int id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(x => x.ID == id);
        }

        public Vehicle GetByVidAndRegNo(string vehicleId, string registrationNo)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(m => m.VehicleId == vehicleId && m.RegistrationNo == registrationNo);
        }

        public IEnumerable<Vehicle> GetByCustomerAndStatus(int? customerId, bool? isConnected)
        {
            var _data = _dbSet.Where(m => (!customerId.HasValue || m.CustomerID == customerId));

            if (isConnected.HasValue && isConnected == true)
            {
                _data = _data.Where(m => m.LastPingTime.HasValue && m.LastPingTime >= DateTime.Now.AddMinutes(-1));
            }
            else if (isConnected.HasValue && isConnected == false)
            {
                _data = _data.Where(m => !m.LastPingTime.HasValue || m.LastPingTime < DateTime.Now.AddMinutes(-1));

            }
            return _data.AsEnumerable();
        }

        public virtual IEnumerable<Vehicle> GetAll()
        {
            return _dbSet;
        }

        public virtual void Update(Vehicle vehicle)
        {
            _db.Entry(vehicle).State = EntityState.Modified;
            _dbSet.Update(vehicle);
        }

        public virtual void Remove(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
