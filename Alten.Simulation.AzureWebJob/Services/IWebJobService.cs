using Alten.Simulation.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Simulation.AzureWebJob.Services
{
   public interface IWebJobService
    {
        Task<int> GenerateVehiclePingSimulation();
    }
}
