using Ecommerce.Business.ViewModels;
using Ecommerce.Data.Entities;
using Ecommerce.Data.Enums;
using Ecommerce.Data.Repositories.Interfaces;

namespace Ecommerce.Business.Services.Implementations
{
    public class OrderService : Interfaces.IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void AddOrder(OrderViewModel viewModel)
        {
            if (viewModel.Cart == null || viewModel.Cart.Products == null || viewModel.Cart.Products.Count == 0)
            {
                throw new Exception("Order must have a cart with at least one product.");
            }
            decimal total = 0;
            foreach (var product in viewModel.Cart.Products)
            {
                total += product.Price;
            }
            var order = new Order
            {
                OrderID = viewModel.OrderID,
                CustomerID = viewModel.CustomerID,
                Cart = new ShoppingCart
                {
                    CartID = viewModel.Cart.CartID,
                    CustomerID = viewModel.Cart.CustomerID,
                    Products = new List<Product>()
                },
                OrderDate = viewModel.OrderDate,
                Status = viewModel.Status,
                PaymentMethod = new Payment
                {
                    Type = viewModel.PaymentMethod.Type,
                    Amount = total,
                    TransactionID = viewModel.PaymentMethod.TransactionID
                }
            };
            foreach (var productViewModel in viewModel.Cart.Products)
            {
                order.Cart.Products.Add(new Product
                {
                    ID = productViewModel.Id,
                    Name = productViewModel.Name,
                    Price = productViewModel.Price
                });
            }
            _orderRepository.AddOrder(order);
        }

        public void DeleteOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
        }

        public List<OrderViewModel> GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders();
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var orderViewModel = new OrderViewModel
                {
                    OrderID = order.OrderID,
                    CustomerID = order.CustomerID,
                    Cart = new ShoppingCartViewModel
                    {
                        CartID = order.Cart.CartID,
                        CustomerID = order.Cart.CustomerID,
                        Products = new List<ProductViewModel>()
                    },
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    PaymentMethod = new PaymentViewModel
                    {
                        Type = order.PaymentMethod.Type,
                        Amount = order.PaymentMethod.Amount,
                        TransactionID = order.PaymentMethod.TransactionID
                    }
                };
                foreach (var product in order.Cart.Products)
                {
                    orderViewModel.Cart.Products.Add(new ProductViewModel
                    {
                        Id = product.ID,
                        Name = product.Name,
                        Price = product.Price
                    });
                }
                orderViewModels.Add(orderViewModel);
            }
            return orderViewModels;
        }

        public List<OrderViewModel> GetOrderByCustomer(int id)
        {
            var orders = _orderRepository.GetOrdersByCustomer(id);
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var orderViewModel = new OrderViewModel
                {
                    OrderID = order.OrderID,
                    CustomerID = order.CustomerID,
                    Cart = new ShoppingCartViewModel
                    {
                        CartID = order.Cart.CartID,
                        CustomerID = order.Cart.CustomerID,
                        Products = new List<ProductViewModel>()
                    },
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    PaymentMethod = new PaymentViewModel
                    {
                        Type = order.PaymentMethod.Type,
                        Amount = order.PaymentMethod.Amount,
                        TransactionID = order.PaymentMethod.TransactionID
                    }
                };
                foreach (var product in order.Cart.Products)
                {
                    orderViewModel.Cart.Products.Add(new ProductViewModel
                    {
                        Id = product.ID,
                        Name = product.Name,
                        Price = product.Price
                    });
                }
                orderViewModels.Add(orderViewModel);
            }
            return orderViewModels;
        }

        public OrderViewModel GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            var orderViewModel = new OrderViewModel
            {
                OrderID = order.OrderID,
                CustomerID = order.CustomerID,
                OrderDate = order.OrderDate,
                Cart = new ShoppingCartViewModel
                {
                    CartID = order.Cart.CartID,
                    CustomerID = order.Cart.CustomerID,
                    Products = new List<ProductViewModel>()
                },
                Status = order.Status,
                PaymentMethod = new PaymentViewModel
                {
                    Type = order.PaymentMethod.Type,
                    Amount = order.PaymentMethod.Amount,
                    TransactionID = order.PaymentMethod.TransactionID,
                }
            };
            foreach (var product in order.Cart.Products)
            {
                orderViewModel.Cart.Products.Add(new ProductViewModel
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                });
            }
            return orderViewModel;
        }

        public void UpdateOrder(OrderViewModel viewModel)
        {
            if (viewModel.Cart == null || viewModel.Cart.Products == null || viewModel.Cart.Products.Count == 0)
            {
                throw new Exception("Order must have a cart with at least one product.");
            }
            decimal total = 0;
            foreach (var product in viewModel.Cart.Products)
            {
                total += product.Price;
            }
            var order = new Order
            {
                OrderID = viewModel.OrderID,
                CustomerID = viewModel.CustomerID,
                Cart = new ShoppingCart
                {
                    CartID = viewModel.Cart.CartID,
                    CustomerID = viewModel.Cart.CustomerID,
                    Products = new List<Product>()
                },
                OrderDate = viewModel.OrderDate,
                Status = viewModel.Status,
                PaymentMethod = new Payment
                {
                    Type = viewModel.PaymentMethod.Type,
                    Amount = total,
                    TransactionID = viewModel.PaymentMethod.TransactionID
                }
            };
            foreach (var productViewModel in viewModel.Cart.Products)
            {
                order.Cart.Products.Add(new Product
                {
                    ID = productViewModel.Id,
                    Name = productViewModel.Name,
                    Price = productViewModel.Price
                });
            }
            _orderRepository.UpdateOrder(order);
        }
    }
}
