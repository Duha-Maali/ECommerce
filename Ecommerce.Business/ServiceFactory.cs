using Ecommerce.Business.Services.Interfaces;
using Ecommerce.Business.Services.Implementations;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Data.Repositories.Implementations;

namespace Ecommerce.Business
{
    // Factory class to create instances of service classes.
    // This class ensures that services are initialized with their required dependencies.
    public static class ServiceFactory
    {
        // Creates an instance of IProductService with a ProductRepository.
        public static IProductService CreateProductService()
        {
            IProductRepository productRepository = new ProductRepository();
            return new ProductService(productRepository);
        }

        // Creates an instance of ICustomerService with a CustomerRepository.
        public static ICustomerService CreateCustomerService()
        {
            ICustomerRepository customerRepository = new CustomerRepository();
            return new CustomerService(customerRepository);
        }

        // Creates an instance of IEmployeeService with an EmployeeRepository.
        public static IEmployeeService CreateEmployeeService()
        {
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            return new EmployeeService(employeeRepository);
        }

        // Creates an instance of IShoppingCartService with a ShoppingCartRepository.
        public static IShoppingCartService CreateShoppingCartService()
        {
            IShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();
            return new ShoppingCartService(shoppingCartRepository);
        }

        // Creates an instance of IOrderService with an OrderRepository.
        public static IOrderService CreateOrderService()
        {
            IOrderRepository orderRepository = new OrderRepository();
            return new OrderService(orderRepository);
        }
    }
}
