using Alten.Simulation.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alten.Simulation.APIClient
{
    public partial class ApiClient
    {
        public async Task<List<VehicleModel>> GetVehicles()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vehicles/GetVehicles"));
            return await GetAsync<List<VehicleModel>>(requestUrl);
        }

        public async Task<MessageModel<VehicleModel>> UpdateVehicle(VehicleModel model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vehicles/UpdateVehicles"));
            return await PostAsync<VehicleModel>(requestUrl, model);
        }

        public async Task<MessageModel<VehiclePingModel>> AddVehiclePing(VehiclePingModel model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, "VehiclesPing/AddPingData"));
            return await PostAsync<VehiclePingModel>(requestUrl, model);
        }
    }
}
