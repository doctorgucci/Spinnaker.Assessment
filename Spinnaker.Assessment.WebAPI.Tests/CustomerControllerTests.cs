using Microsoft.AspNetCore.Mvc;
using Moq;
using Spinnaker.Asessment.WebAPI.Controllers;
using Spinnaker.Asessment.WebAPI.Infrastructure.Exceptions;
using Spinnaker.Assessment.DomainModels;
using Spinnaker.Assessment.Interfaces.In;

namespace Spinnaker.Assessment.WebAPI.Tests
{
    public class CustomerControllerTests
    {
        [Fact]
        public async Task Get_Customers_ReturnsListOfCustomers()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(service => service.GetCustomers())
                       .ReturnsAsync(new List<Customer> { new Customer(), new Customer() });
            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.Customers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customers = Assert.IsAssignableFrom<IEnumerable<Customer>>(okResult.Value);
            Assert.Equal(2, customers.Count());
        }

        [Fact]
        public async Task Get_CustomerById_ReturnsCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(service => service.GetCustomerById(customerId))
                       .ReturnsAsync(new Customer { Id = customerId, Name = "John Doe" });
            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.GetbyId(customerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customer = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(customerId, customer.Id);
        }

        [Fact]
        public async Task Get_CustomerById_ReturnsNotFound_ForNonExistingCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var mockService = new Mock<ICustomerService>();
            _ = mockService.Setup(service => service.GetCustomerById(customerId))
                       .ReturnsAsync((Customer)null);
            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.GetbyId(customerId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Customer with ID {customerId} not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task Create_Customer_ReturnsCreatedCustomer()
        {
            // Arrange
            var newCustomer = new Customer { Name = "Alice" };
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(service => service.CreateCustomer(newCustomer))
                       .ReturnsAsync(newCustomer);
            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.Create(newCustomer);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var customer = Assert.IsType<Customer>(createdAtActionResult.Value);
            Assert.Equal(newCustomer.Name, customer.Name);
        }

        [Fact]
        public async Task Update_Customer_ReturnsUpdatedCustomer()
        {
            // Arrange
            var existingCustomerId = Guid.NewGuid();
            var updatedCustomer = new Customer { Id = existingCustomerId, Name = "Bob" };
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(service => service.UpdateCustomer(updatedCustomer))
                       .ReturnsAsync(updatedCustomer);
            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.Update(existingCustomerId, updatedCustomer);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customer = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(existingCustomerId, customer.Id);
            Assert.Equal(updatedCustomer.Name, customer.Name);
        }    

        [Fact]
        public async Task Delete_Customer_ReturnsNotFound_ForNonExistingCustomer()
        {
            // Arrange
            var nonExistingCustomerId = Guid.NewGuid();
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(service => service.DeleteCustomer(nonExistingCustomerId))
                       .Throws<CustomerNotFoundException>();
            var controller = new CustomerController(mockService.Object);

            // Act
            var result = await controller.Delete(nonExistingCustomerId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Customer with ID {nonExistingCustomerId} not found.", notFoundResult.Value);
        }
    }
}