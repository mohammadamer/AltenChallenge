using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alten.Vehicles.Domain.Interfaces.Repository;
using Alten.Vehicles.Domain.Interfaces.Services;
using Alten.Vehicles.Domain.Services;
using Alten.Vehicles.Infra.Data.Context;
using Alten.Vehicles.Infra.Data.Repository;
using Alten.Vehicles.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alten.Vehicles
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CorsPolicyBuilder corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();
            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper(x => x.AddProfile(new MappingEntity()));

            services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<VehicleDbContext, VehicleDbContext>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<IVehiclePingRepository, VehiclePingRepository>();
            services.AddTransient<IVehiclePingService, VehiclePingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("SiteCorsPolicy");
            app.UseMvc();
        }
    }
}
