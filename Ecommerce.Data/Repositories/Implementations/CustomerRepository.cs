using System;
using System.Collections.Generic;
using System.Linq;
using Ecommerce.Data.Entities;
using Ecommerce.Data.Repositories.Interfaces;
using System.Text.Json;
using Ecommerce.Data.Utilities;

namespace Ecommerce.Data.Repositories.Implementations
{
    public class CustomerRepository : Interfaces.ICustomerRepository
    {
        private const string FileName = "customers.json";
        private readonly List<Customer> _customers;
        public CustomerRepository()
        {
            try
            {
                var customerList = FileHandler.ReadJson<List<Customer>>(FileName);
                if (customerList == null)
                {
                    _customers = new List<Customer>();
                }
                else
                {
                    _customers = customerList;
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"Failed to read file {FileName}: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to deserialize JSON from {FileName}: {ex.Message}", ex);
            }
        }
        public void AddCustomer(Customer customer)
        {
            if (_customers.Any(c => c.CustomerID == customer.CustomerID))
            {
                throw new Exception($"Customer with this ID {customer.CustomerID} already exists.");
            }
            _customers.Add(customer);
            SaveChanges();
        }
        public Customer GetCustomerByID(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerID == id);
            return customer;
        }
        public List<Customer> GetAllCustomers()
        {
            return _customers;
        }

        public void UpdateCustomer(Customer updatedCustomer)
        {
            var existingCustomer = _customers.FirstOrDefault(c => c.CustomerID == updatedCustomer.CustomerID);
            if (existingCustomer == null)
            {
                throw new Exception("Customer not found.");
            }
            existingCustomer.Name = updatedCustomer.Name;
            SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            _customers.Remove(customer);
            SaveChanges();
        }

        private void SaveChanges()
        {
            try
            {
                FileHandler.WriteJson(FileName, _customers);
            }
            catch (IOException ex)
            {
                throw new IOException($"Failed to write to file {FileName}: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to serialize JSON to {FileName}: {ex.Message}", ex);
            }
        }
    }
}
