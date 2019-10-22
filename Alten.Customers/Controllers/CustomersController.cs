using Alten.Customers.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alten.Customers.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        // GET: api/GetCustomers
        [HttpGet]
        [Route("GetCustomers")]

        public IActionResult GetCustomers()
        {
            var customers = _service.GetAll().ToList();
            return Ok(customers);
        }
    }
}
