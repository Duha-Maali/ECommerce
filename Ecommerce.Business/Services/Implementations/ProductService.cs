using Ecommerce.Business.ViewModels;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Data.Entities;

namespace Ecommerce.Business.Services.Implementations
{
    public class ProductService : Interfaces.IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddProduct(ProductViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "Product view model cannot be null.");
            }
            if (viewModel.Price <= 0)
            {
                throw new Exception("Price must be greater than zero.");
            }
            var product = new Product
            {
                ID = viewModel.Id,
                Name = viewModel.Name,
                Price = viewModel.Price
            };
            _productRepository.AddProduct(product);
        }

        public void DeleteProduct(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid product ID.", nameof(id));
            }
            _productRepository.DeleteProduct(id);
        }

        public List<ProductViewModel> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            var productViewModels = new List<ProductViewModel>();
            foreach (var product in products)
            {
                productViewModels.Add(new ProductViewModel
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price
                });

            }
            return productViewModels;
        }

        public ProductViewModel GetProductById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid product ID.", nameof(id));
            }
            var product = _productRepository.GetProductById(id);
            return new ProductViewModel
            {
                Id = product.ID,
                Name = product.Name,
                Price = product.Price
            };
        }

        public void UpdateProduct(ProductViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "Product view model cannot be null.");
            }
            if (viewModel.Price <= 0)
            {
                throw new Exception("Price must be greater than zero");
            }
            var product = new Product
            {
                ID = viewModel.Id,
                Name = viewModel.Name,
                Price = viewModel.Price
            };
            _productRepository.UpdateProduct(product);
        }
    }
}
