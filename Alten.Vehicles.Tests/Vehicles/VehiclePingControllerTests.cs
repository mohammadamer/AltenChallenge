using Alten.Vehicles.Controllers;
using Alten.Vehicles.Domain.Entities;
using Alten.Vehicles.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Vehicles.Tests.Vehicles
{
    public class VehiclePingControllerTests
    {
        private VehiclePing vehiclePing;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            vehiclePing = new VehiclePing
            {
                
                VehicleID = 1,
                PingDate = DateTime.Now
            };
        }

        [Test]
        [Category("VehiclePingTestsCategory")]
        public void AddPingData_WithReturnOkResult()
        {
            //Arrange
            var mock = new Mock<IVehiclePingService>();
            mock.Setup(p => p.Add(vehiclePing));
            VehiclesPingController vehiclePingController = new VehiclesPingController(mock.Object);

            //Act
            var sut = vehiclePingController.AddPingData(vehiclePing);
            var okResult = sut as OkResult;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

    }
}
