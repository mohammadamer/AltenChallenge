using Alten.Vehicles.Domain.DataModels;
using Alten.Vehicles.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alten.Vehicles.Mappers
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            CreateMap<VehicleViewModel, Vehicle>();
            CreateMap<Vehicle, VehicleViewModel>();
        }
    }
}
