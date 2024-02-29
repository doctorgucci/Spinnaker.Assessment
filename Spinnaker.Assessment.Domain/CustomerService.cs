using Spinnaker.Assessment.DomainModels;
using Spinnaker.Assessment.Interfaces.In;
using Spinnaker.Assessment.Interfaces.Out;

namespace Spinnaker.Assessment.Domain
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            return await _customerRepo.CreateCustomer(customer);
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            if(customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            return await _customerRepo.UpdateCustomer(customer);
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _customerRepo.GetCustomers();
        }

        public async Task<Customer?> DeleteCustomer(Guid customerId)
        {
            return await _customerRepo.DeleteCustomer(customerId);
        }

        public async Task<Customer> GetCustomerById(Guid guid)
        {
            ArgumentNullException.ThrowIfNull(nameof(guid));
            return await _customerRepo.GetCustomerById(guid);
        }
    }
}
