using Microsoft.EntityFrameworkCore;
using Spinnaker.Asessment.SQLIntegration.Base;
using Spinnaker.Asessment.SQLIntegration.Entities;
using Spinnaker.Assessment.DomainModels;
using Spinnaker.Assessment.Interfaces.Out;
using System;


namespace Spinnaker.Asessment.SQLIntegration.Repos
{
    public class CustomerRepo(CustomerDbContext context) : BaseRepo(context), ICustomerRepo
    {
        public async Task<Customer> GetCustomerById(Guid id)
        {
            var existingCustomer = await context.Customers
                .Include(c => c.Countries)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCustomer == null)
                return null;

            return MapCustomerEntityToModel(existingCustomer);
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var customers = await context.Customers
                .Include(c => c.Countries)
                .ToListAsync();

            return customers.Select(c => MapCustomerEntityToModel(c)).ToList();
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(customer));

            var entity = new CustomerEntity
            {
                Name = customer.Name,
                Surname = customer.Surname,
                IdentityNumber = customer.IdentityNumber,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CountriesId = customer.CountryId
            };

            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(customer));

            var existingCustomer = await context.Customers.FindAsync(customer.Id);
            if (existingCustomer != null)
            {
                MapCustomerModelToEntity(customer, existingCustomer);
                _context.Customers.Update(existingCustomer);
                await _context.SaveChangesAsync();
            }

            return customer;
        }

        public async Task<Customer?> DeleteCustomer(Guid customerId)
        {
            var customer = await context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

            return MapCustomerEntityToModel(customer);
        }

        private static Customer? MapCustomerEntityToModel(CustomerEntity entity)
        {
            var customer = new Customer
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                IdentityNumber = entity.IdentityNumber,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                CountryId = entity.CountriesId
            };

            if (entity.Countries is not null)
            {
                customer.Country = new Country
                {
                    Id = entity.Countries.Id,
                    Iso = entity.Countries.Iso,
                    Name = entity.Countries.Name,
                    FriendlyName = entity.Countries.FriendlyName,
                    NumCode = entity.Countries.NumCode,
                    PhoneCode = entity.Countries.PhoneCode
                };
            }
            return customer;
        }

        private static void MapCustomerModelToEntity(Customer customer, CustomerEntity entity)
        {
            entity.Name = customer.Name;
            entity.Surname = customer.Surname;
            entity.IdentityNumber = customer.IdentityNumber;
            entity.Email = customer.Email;
            entity.PhoneNumber = customer.PhoneNumber;
            entity.CountriesId = customer.CountryId;
        }
    }
}

