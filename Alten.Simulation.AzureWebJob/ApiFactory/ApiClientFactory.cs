using Alten.Simulation.APIClient;
using Alten.Simulation.AzureWebJob.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Alten.Simulation.AzureWebJob.ApiFactory
{
    public static class ApiClientFactory
    {
        private static Uri apiUri;

        private static Lazy<ApiClient> restClient = new Lazy<ApiClient>(
          () => new ApiClient(apiUri),
          LazyThreadSafetyMode.ExecutionAndPublication);

        static ApiClientFactory()
        {
            apiUri = new Uri(ApplicationSettings.WebApiUrl);
        }

        public static ApiClient Instance
        {
            get
            {
                return restClient.Value;
            }
        }
    }
}
