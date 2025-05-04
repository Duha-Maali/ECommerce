using Ecommerce.Business.ViewModels;

namespace Ecommerce.Business.Services.Interfaces
{
    public interface IProductService
    {
        void AddProduct(ProductViewModel viewModel);
        ProductViewModel GetProductById(int id);
        List<ProductViewModel> GetAllProducts();
        void UpdateProduct(ProductViewModel viewModel);
        void DeleteProduct(int id);
    }
}
