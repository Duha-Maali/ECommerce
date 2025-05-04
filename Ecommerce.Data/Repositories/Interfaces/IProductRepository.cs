
namespace Ecommerce.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Entities.Product product);
        Entities.Product GetProductById(int id);
        List<Entities.Product> GetAllProducts();
        void UpdateProduct(Entities.Product product);
        void DeleteProduct(int id);
    }
}
