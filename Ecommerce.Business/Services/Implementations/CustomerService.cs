using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Data.Entities;
using Ecommerce.Business.ViewModels;

namespace Ecommerce.Business.Services.Implementations
{
    public class CustomerService : Interfaces.ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddCustomer(CustomerViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "Customer view model cannot be null.");
            }
            var customer = new Customer
            {
                CustomerID = viewModel.CustomerID,
                Name = viewModel.Name
            };
            _customerRepository.AddCustomer(customer);
        }

        public void DeleteCustomer(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Customer ID.", nameof(id));
            }
            _customerRepository.DeleteCustomer(id);
        }

        public List<CustomerViewModel> GetAllCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();
            var customerViewModels = new List<CustomerViewModel>();
            foreach (var customer in customers)
            {
                customerViewModels.Add(new CustomerViewModel
                {
                    CustomerID = customer.CustomerID,
                    Name = customer.Name
                });
            }
            return customerViewModels;
        }

        public CustomerViewModel GetCustomerById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Customer ID.", nameof(id));
            }
            
            var customer = _customerRepository.GetCustomerByID(id);
            if (customer == null)
            {
                return null;
            }
            return new CustomerViewModel
            {
                CustomerID = customer.CustomerID,
                Name = customer.Name
            };
        }

        public void UpdateCustomer(CustomerViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "Customer view model cannot be null.");
            }
            var customer = new Customer
            {
                CustomerID = viewModel.CustomerID,
                Name = viewModel.Name
            };
            _customerRepository.UpdateCustomer(customer);
        }
    }
}
