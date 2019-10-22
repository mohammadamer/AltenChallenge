using Alten.Customers.Controllers;
using Alten.Customers.Domain.Entities;
using Alten.Customers.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alten.Customers.Tests.Customers
{
    public class CustomersControllerTests
    {
        private List<Customer> customers;

        [Test]
        [Category("CustomerTestsCategory")]
        public void GetCustomers_WithReturnOkResult()
        {
            //Arrange
            var mock = new Mock<ICustomerService>();
            mock.Setup(p => p.GetAll());
            CustomersController customerController = new CustomersController(mock.Object);

            //Act
            var sut = customerController.GetCustomers();
            var okResult = sut as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
             customers = new List<Customer>
            {
                 new Customer {  Name = "Kalles Grustransporter AB", Address = "Cementvägen 8, 111 11 Södertälje", CreatedOn = DateTime.Now },
                 new Customer {  Name = "Johans Bulk AB", Address = "Balkvägen 12, 222 22 Stockholm", CreatedOn = DateTime.Now },
                 new Customer {  Name = "Haralds Värdetransporter AB", Address = "Budgetvägen 1, 333 33 Uppsala", CreatedOn = DateTime.Now }
            };
        }

        [Test]
        [Category("CustomerTestsCategory")]
        public void GetCustomers_WithGetAll()
        {
            //Arrange
            const int customerCount = 3;

            var mock = new Mock<ICustomerService>();
            mock.Setup(p => p.GetAll()).Returns(customers);
            CustomersController customerController = new CustomersController(mock.Object);

            //Act
            var sut = customerController.GetCustomers();
            var okResult = sut as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(customerCount, customers.Count);
        }
    }
}
