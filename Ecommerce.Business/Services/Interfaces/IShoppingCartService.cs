using Ecommerce.Business.ViewModels;

namespace Ecommerce.Business.Services.Interfaces
{
    public interface IShoppingCartService
    {
        void AddShoppingCart(ShoppingCartViewModel viewModel);
        ShoppingCartViewModel GetShoppingCartById(int id);
        ShoppingCartViewModel GetShoppingCartByCustomer(int customerId);
        List<ShoppingCartViewModel> GetAllShoppingCarts();
        void UpdateShoppingCart(ShoppingCartViewModel viewModel);
        void DeleteShoppingCart(int id);
    }
}
