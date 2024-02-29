using Spinnaker.Assessment.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinnaker.Assessment.Interfaces.In
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<List<Customer>> GetCustomers();
        Task<Customer?> DeleteCustomer(Guid customerId);
        Task<Customer> GetCustomerById(Guid guid);
    }
}
