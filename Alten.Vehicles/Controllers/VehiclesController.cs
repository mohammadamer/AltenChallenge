using Alten.Vehicles.Domain.DataModels;
using Alten.Vehicles.Domain.Entities;
using Alten.Vehicles.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alten.Vehicles.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _service;
        private readonly IMapper _mapper;
        public VehiclesController(IVehicleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("GetVehicles")]
        public IActionResult GetVehicles(FilterVehicles filterVehicles)
        {
            IEnumerable<Vehicle> vehicles;
            if (filterVehicles == null || (filterVehicles.CustomerID == 0 && filterVehicles.IsConnected == 0))
            {
                vehicles = _service.GetAll();
            }
            else
            {
                int? cutomerId = filterVehicles.CustomerID < 0 ? (int?)null : filterVehicles.CustomerID;
                bool? isConnected = filterVehicles.IsConnected < 0 ? (bool?)null : (filterVehicles.IsConnected > 0 ? true : false);
                vehicles = _service.GetByCustomerAndStatus(cutomerId, isConnected);
            }

            return Ok(_mapper.Map<List<VehicleViewModel>>(vehicles));
        }


        [HttpPost]
        [Route("UpdateVehicles")]
        public IActionResult UpdateVehicles(Vehicle vehicleModel)
        {
            if (vehicleModel != null )
            {
                 _service.Update(vehicleModel);
                return Ok();
            }
            return BadRequest();
            
        }
    }
}
