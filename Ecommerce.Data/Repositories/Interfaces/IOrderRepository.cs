
namespace Ecommerce.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Entities.Order order);
        Entities.Order GetOrderById(int id);
        List<Entities.Order> GetOrdersByCustomer(int customerId);
        List<Entities.Order> GetAllOrders();
        void UpdateOrder(Entities.Order order);
        void DeleteOrder(int id);
    }
}
