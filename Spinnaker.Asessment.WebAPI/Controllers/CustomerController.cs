using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spinnaker.Asessment.WebAPI.Infrastructure.Exceptions;
using Spinnaker.Assessment.DomainModels;
using Spinnaker.Assessment.Interfaces.In;

namespace Spinnaker.Asessment.WebAPI.Controllers
{
    /// <summary>
    /// API controller for managing customers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="customerService">The customer service.</param>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        /// <summary>
        /// Retrieves a list of customers.
        /// </summary>
        /// <returns>A list of customers.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Customers()
        {
            try
            {
                var result = await _customerService.GetCustomers();
                return Ok(result);
            }
            catch (Exception ex)
            {               
                return StatusCode(500, $"An unexpected error occurred while retrieving customers: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a customer by ID.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>The customer with the specified ID.</returns>
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetbyId(Guid customerId)
        {
            try
            {
                var result = await _customerService.GetCustomerById(customerId);
                if (result == null)
                {
                    return NotFound($"Customer with ID {customerId} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {               
                return StatusCode(500, $"An unexpected error occurred while retrieving customer with ID {customerId}: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">The customer to create.</param>
        /// <returns>The created customer.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Customer), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (customer == null || string.IsNullOrEmpty(customer.Name))
                return BadRequest("Name is required for creating a new customer.");

            try
            {
                var result = await _customerService.CreateCustomer(customer);
                return CreatedAtAction(nameof(GetbyId), new { customerId = result.Id }, result);
            }
            catch (Exception ex)
            {               
                return StatusCode(500, $"An unexpected error occurred while creating the customer: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer to delete.</param>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [HttpDelete("{customerId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(Guid customerId)
        {
            try
            {
                await _customerService.DeleteCustomer(customerId);
                return NoContent();
            }
            catch (CustomerNotFoundException)
            {
                return NotFound($"Customer with ID {customerId} not found.");
            }
            catch (Exception ex)
            {              
                return StatusCode(500, $"An unexpected error occurred while deleting the customer: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="customer">The updated customer data.</param>
        /// <returns>The updated customer.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(Guid id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest("ID mismatch between route parameter and customer object.");
            }

            if (string.IsNullOrEmpty(customer.Name))
            {
                return BadRequest("Name is required for updating a customer.");
            }

            try
            {
                var result = await _customerService.UpdateCustomer(customer);
                if (result == null)
                {
                    return NotFound($"Customer with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {              
                return StatusCode(500, $"An unexpected error occurred while updating the customer: {ex.Message}");
            }
        }
    }
}
