using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Alten.Simulation.AzureWebJob.Services;
using Alten.Simulation.AzureWebJob.Utility;
using Alten.Simulation.Models.Models;
using Alten.Vehicles.Domain.Interfaces.Services;
using Alten.Vehicles.Domain.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alten.Simulation.AzureWebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            var config = new JobHostConfiguration
            {
                JobActivator = new CustomJobActivator(services.BuildServiceProvider())
            };
            config.UseTimers();

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            var host = new JobHost(config);
            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection )
        {
            var configuration = GetWebJobConfiguration();

            // DI-Configuration
            serviceCollection.AddHttpClient();
            serviceCollection.AddScoped<ScheduledWebJob>();
            
            serviceCollection.Configure<SettingsModel>(configuration.GetSection("WebApiSettings"));
            serviceCollection.AddScoped<IWebJobService, WebJobService>();
            AddWebJobsCommonServices(configuration);
        }

        private static void AddWebJobsCommonServices(IConfigurationRoot configuration)
        {
            if (String.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("AzureWebJobsStorage")))
            {
                // Env variables would be set on azure. But not locally. If missing, set them to the connection string
                Environment.SetEnvironmentVariable("AzureWebJobsStorage", configuration.GetConnectionString("AzureWebJobsStorage"));
                Environment.SetEnvironmentVariable("AzureWebJobsDashboard", configuration.GetConnectionString("AzureWebJobsDashboard"));
                Environment.SetEnvironmentVariable("AzureWebJobsDashboard", configuration.GetConnectionString("AzureWebJobsDashboard"));
            }
        }

        private static IConfigurationRoot GetWebJobConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return configuration;
        }
    }
}