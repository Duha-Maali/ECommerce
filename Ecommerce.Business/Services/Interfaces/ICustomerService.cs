using Ecommerce.Business.ViewModels;

namespace Ecommerce.Business.Services.Interfaces
{
    public interface ICustomerService
    {
        void AddCustomer(CustomerViewModel viewModel);
        CustomerViewModel GetCustomerById(int id);
        List<CustomerViewModel> GetAllCustomers();
        void UpdateCustomer(CustomerViewModel viewModel);
        void DeleteCustomer(int id);
    }
}
