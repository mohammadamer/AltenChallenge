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
    public class VehiclePingRepository : IVehiclePingRepository
    {
        private readonly VehicleDbContext _db;
        private readonly DbSet<VehiclePing> _dbSet;
        public VehiclePingRepository(VehicleDbContext context)
        {
            _db = context;
            _dbSet = _db.Set<VehiclePing>();
        }

        public virtual void Add(VehiclePing vehiclePing)
        {
            _dbSet.Add(vehiclePing);
        }

        public virtual IEnumerable<VehiclePing> GetAll()
        {
            return _dbSet;
        }

        public VehiclePing GetById(int id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(x => x.ID == id);
        }

        public virtual void Update(VehiclePing vehiclePing)
        {
            _db.Entry(vehiclePing).State = EntityState.Modified;
            _dbSet.Update(vehiclePing);
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
