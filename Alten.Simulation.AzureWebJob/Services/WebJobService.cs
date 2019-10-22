
using Alten.Simulation.AzureWebJob.ApiFactory;
using Alten.Simulation.AzureWebJob.Utility;
using Alten.Simulation.Models.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Simulation.AzureWebJob.Services
{
    public class WebJobService : IWebJobService
    {

        private readonly IOptions<SettingsModel> appSettings;
        public WebJobService(IOptions<SettingsModel> app)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
        }

        public async Task<int> GenerateVehiclePingSimulation()
        {
            var _connectedVehicleCount = 0;

            // Get All Vehicle 
            IEnumerable<VehicleModel> _vehicles = await ApiClientFactory.Instance.GetVehicles();

            var SimulatedVehicles = new List<VehicleModel>();
            // Randomly select the ones which will be counted as connected
            foreach (var item in _vehicles)
            {
                Random rand = new Random();
                if (rand.Next(0, 2) != 0)
                {
                    DateTime _datetime = DateTime.Now;

                    // Update Vehicle if with last successful ping data time
                    item.LastPingTime = _datetime;
                    var UpdateVehicleResponse = await ApiClientFactory.Instance.UpdateVehicle(item);

                    // Add Vehicle ping successful date time
                    VehiclePingModel pinglog = new VehiclePingModel
                    {
                        VehicleID = item.ID,
                        PingDate = _datetime
                    };
                    var VehiclePingResponse = await ApiClientFactory.Instance.AddVehiclePing(pinglog);


                    _connectedVehicleCount++;
                }

            }
            return _connectedVehicleCount;

        }

    }
}
