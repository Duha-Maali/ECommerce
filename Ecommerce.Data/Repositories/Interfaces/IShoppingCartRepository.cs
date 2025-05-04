
namespace Ecommerce.Data.Repositories.Interfaces
{
    public interface IShoppingCartRepository
    {
        void AddShoppingCart(Entities.ShoppingCart cart);
        Entities.ShoppingCart GetShoppingCartById(int id);
        Entities.ShoppingCart GetShoppingCartByCustomer(int customerId);
        List<Entities.ShoppingCart> GetAllShoppingCarts();
        void UpdateShoppingCart(Entities.ShoppingCart cart);
        void DeleteShoppingCart(int id);
    }
}
