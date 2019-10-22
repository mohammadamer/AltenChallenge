using Alten.Vehicles.Domain.Entities;
using Alten.Vehicles.Domain.Interfaces.Services;
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
    public class VehiclesPingController : ControllerBase
    {
        private readonly IVehiclePingService _service;
        public VehiclesPingController(IVehiclePingService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("AddPingData")]
        public IActionResult AddPingData(VehiclePing vehiclePing)
        {
            if (vehiclePing != null)
            {
                _service.Add(vehiclePing);
                return Ok();
            }
            return BadRequest();

        }
    }
}
