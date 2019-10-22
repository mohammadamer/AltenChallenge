using System;
using System.Collections.Generic;
using System.IO;
using Alten.Simulation.AzureWebJob.Services;
using Alten.Simulation.Models.Models;
using Microsoft.Azure.WebJobs;

namespace Alten.Simulation.AzureWebJob
{
    public class ScheduledWebJob
    {
        private readonly IWebJobService _webJobService;

       public ScheduledWebJob(IWebJobService webJobService)
        {
            _webJobService = webJobService;
        }

        public void StartScheduledJob([TimerTrigger("0 * * * * *", RunOnStartup = true)] TimerInfo timerInfo, TextWriter log)
        {
            try
            {
                Console.WriteLine($"Vehicle Ping Simulation Web Job Started.");
              
                var _countOfConnectedVehicles = _webJobService.GenerateVehiclePingSimulation();

                Console.WriteLine($"Vehicle Ping Simulation Web Job Ran Successfully.");
                log.WriteLine($"Number of Connected Vehicle/Vehicles is/are " + $"{_countOfConnectedVehicles}");
            }
            catch (Exception)
            {
                throw;
                //log.WriteLine($"Vehicle Ping Simulation Web Job Has failed.");
            }
        }
    }
}