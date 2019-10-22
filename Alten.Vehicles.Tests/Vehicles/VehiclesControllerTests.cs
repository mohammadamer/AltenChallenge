using Alten.Vehicles.Controllers;
using Alten.Vehicles.Domain.DataModels;
using Alten.Vehicles.Domain.Entities;
using Alten.Vehicles.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Vehicles.Tests.Vehicles
{
    public class VehiclesControllerTests
    {
        private Vehicle vehicle;

        [Test]
        [Category("VehiclesTestsCategory")]
        public void GetVehicles_WithReturnOkResult()
        {
            //Arrange
            FilterVehicles filterVehicle = new FilterVehicles()
            {
                CustomerID = 1,
                IsConnected = 1
            };
            var mockIMapper = new Mock<IMapper>();

            var mockIVehicleService = new Mock<IVehicleService>();
            mockIVehicleService.Setup(p => p.GetAll());
            VehiclesController vehiclesController = new VehiclesController(mockIVehicleService.Object, mockIMapper.Object);

            //Act
            var sut = vehiclesController.GetVehicles(filterVehicle);
            var okResult = sut as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            vehicle = new Vehicle
            {
                ID = 1,
                VehicleId = "YS2R4X20005399401",
                RegistrationNo = "ABC123",
                CustomerID = 1,
                CustomerName = "Kalles Grustransporter AB",
                LastPingTime = DateTime.Now
            };
        }

        [Test]
        [Category("VehiclesTestsCategory")]
        public void UpdateVehicles_WithReturnOkResult()
        {
            //Arrange
            var mockIMapper = new Mock<IMapper>();

            var mockIVehicleService = new Mock<IVehicleService>();
            mockIVehicleService.Setup(p => p.Update(vehicle));
            VehiclesController vehiclesController = new VehiclesController(mockIVehicleService.Object, mockIMapper.Object);

            //Act
            var sutResult = vehiclesController.UpdateVehicles(vehicle);
            var okResult = sutResult as OkResult;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }


    }
}
