
namespace Ecommerce.Data.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        void AddCustomer(Entities.Customer customer);
        Entities.Customer GetCustomerByID(int id);
        List<Entities.Customer> GetAllCustomers();
        void UpdateCustomer(Entities.Customer customer);
        void DeleteCustomer(int id);
    }
}
