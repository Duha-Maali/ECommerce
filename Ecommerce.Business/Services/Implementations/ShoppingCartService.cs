using Ecommerce.Business.ViewModels;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Data.Entities;

namespace Ecommerce.Business.Services.Implementations
{
    public class ShoppingCartService : Interfaces.IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public void AddShoppingCart(ShoppingCartViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "Shopping Cart cannot be null.");
            }
            var cart = new ShoppingCart
            {
                CartID = viewModel.CartID,
                CustomerID = viewModel.CustomerID,
                Products = new List<Product>()
            };
            if (viewModel.Products != null || viewModel.Products.Count != 0)
            {
                foreach (var productViewModel in viewModel.Products)
                {
                    cart.Products.Add(new Product
                    {
                        ID = productViewModel.Id,
                        Name = productViewModel.Name,
                        Price = productViewModel.Price
                    });
                }
            }
            _shoppingCartRepository.AddShoppingCart(cart);
        }

        public void DeleteShoppingCart(int id)
        {
            _shoppingCartRepository.DeleteShoppingCart(id);
        }

        public List<ShoppingCartViewModel> GetAllShoppingCarts()
        {
            var carts = _shoppingCartRepository.GetAllShoppingCarts();
            var cartViewModels = new List<ShoppingCartViewModel>();
            foreach (var cart in carts)
            {
                var cartViewModel = new ShoppingCartViewModel
                {
                    CartID = cart.CartID,
                    CustomerID = cart.CustomerID,
                    Products = new List<ProductViewModel>()
                };
                foreach (var product in cart.Products)
                {
                    cartViewModel.Products.Add(new ProductViewModel
                    {
                        Id = product.ID,
                        Name = product.Name,
                        Price = product.Price
                    });
                }
                cartViewModels.Add(cartViewModel);
            }
            return cartViewModels;
        }

        public ShoppingCartViewModel GetShoppingCartByCustomer(int customerId)
        {
            var cart = _shoppingCartRepository.GetShoppingCartByCustomer(customerId);
            var cartViewModel = new ShoppingCartViewModel
            {
                CartID = cart.CartID,
                CustomerID = cart.CustomerID,
                Products = new List<ProductViewModel>()
            };
            foreach (var product in cart.Products)
            {
                cartViewModel.Products.Add(new ProductViewModel
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price
                });
            }
            return cartViewModel;
        }

        public ShoppingCartViewModel GetShoppingCartById(int id)
        {
            var cart = _shoppingCartRepository.GetShoppingCartById(id);
            var cartViewModel = new ShoppingCartViewModel
            {
                CartID = cart.CartID,
                CustomerID = cart.CustomerID,
                Products = new List<ProductViewModel>()
            };
            foreach (var product in cart.Products)
            {
                cartViewModel.Products.Add(new ProductViewModel
                {
                    Id = cart.CartID,
                    Name = product.Name,
                    Price = product.Price
                });
            }
            return cartViewModel;
        }

        public void UpdateShoppingCart(ShoppingCartViewModel viewModel)
        {
            var cart = new ShoppingCart
            {
                CartID = viewModel.CartID,
                CustomerID = viewModel.CustomerID,
                Products = new List<Product>()
            };
            if (viewModel.Products != null || viewModel.Products.Count != 0)
            {
                foreach (var productViewModel in viewModel.Products)
                {
                    cart.Products.Add(new Product
                    {
                        ID = productViewModel.Id,
                        Name = productViewModel.Name,
                        Price = productViewModel.Price
                    });
                }
            }
            _shoppingCartRepository.UpdateShoppingCart(cart);
        }
    }
}
