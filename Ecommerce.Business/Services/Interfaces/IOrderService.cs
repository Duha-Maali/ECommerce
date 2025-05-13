using Ecommerce.Business.ViewModels;

namespace Ecommerce.Business.Services.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(OrderViewModel viewModel);
        OrderViewModel GetOrderById(int id);
        List<OrderViewModel> GetAllOrders();
        List<OrderViewModel> GetOrderByCustomer(int id);
        void UpdateOrder(OrderViewModel viewModel);
        void DeleteOrder(int id);
    }
}
