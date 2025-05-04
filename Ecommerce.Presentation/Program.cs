using System;
using System.Text.Json;
using Ecommerce.Business;
using Ecommerce.Business.Services.Implementations;
using Ecommerce.Business.Services.Interfaces;
using Ecommerce.Business.ViewModels;
using Ecommerce.Data.Enums;

public class Program
{
    public static void Main(string[] args)
    {
        IProductService productService = ServiceFactory.CreateProductService();
        ICustomerService customerService = ServiceFactory.CreateCustomerService();
        IEmployeeService employeeService = ServiceFactory.CreateEmployeeService();
        IShoppingCartService shoppingCartService = ServiceFactory.CreateShoppingCartService();
        IOrderService orderService = ServiceFactory.CreateOrderService();

        Console.WriteLine("==== Welcome to E-Commerce App ====");
        Console.WriteLine("Enter as:");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Employee");
        Console.WriteLine("Select role (1-2): ");

        string roleChoice = Console.ReadLine();
        if (roleChoice != "1" && roleChoice != "2")
        {
            Console.WriteLine("Invalid role. Exiting...");
            return;
        }

        Console.WriteLine("1. Log in");
        Console.WriteLine("2. Sign up");
        Console.WriteLine("Select an option (1-2): ");

        string authChoice = Console.ReadLine();
        if (authChoice != "1" && authChoice != "2")
        {
            Console.WriteLine("Invalid option. Exiting...");
            return;
        }

        // Customer
        if (roleChoice == "1")
        {
            CustomerViewModel customer;
            //Log in
            if (authChoice == "1")
            {
                Console.Write("Enter your Customer ID:");
                int customerId;
                try
                {
                    customerId = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid ID format. Exiting...");
                    return;
                }
                customer = customerService.GetCustomerById(customerId);
                if (customer == null)
                {
                    Console.WriteLine("Customer not found. Please check the ID and try again.");
                    return;
                }
                Console.WriteLine($"Welcome Back, {customer.Name}!");
            }
            //Sign up
            else
            {
                customer = SignUpCustomer(customerService, shoppingCartService);
            }
            CustomerMenu(customer.CustomerID, productService, shoppingCartService, orderService);
        }
        // Employee
        else
        {
            EmployeeViewModel employee;
            //Log in
            if (authChoice == "1")
            {
                Console.Write("Enter your Employee ID: ");
                int employeeId;
                try
                {
                    employeeId = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid ID format. Exiting...");
                    return;
                }
                
                employee = employeeService.GetEmployeeById(employeeId);
                employee = employeeService.GetEmployeeById(employeeId);
                if (employee == null)
                {
                    Console.WriteLine("Employee not found. Please check the ID and try again.");
                    return;
                }
                Console.WriteLine($"Welcome Back, {employee.Name}!");
            }
            //Sign up
            else
            {
                employee = SignUpEmployee(employeeService);
            }
            EmployeeMenu(productService, customerService, orderService);
        }
    }

    public static CustomerViewModel SignUpCustomer(ICustomerService customerService, IShoppingCartService shoppingCartService)
    {
        int id;
        while (true)
        {
            Console.Write("Enter new Customer ID: ");
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid ID format. Please enter a valid numeric ID.");
                continue;
            }

            if (customerService.GetCustomerById(id) != null)
            {
                Console.WriteLine("Customer ID already exists. Try a different ID.");
                continue;
            }
            break;
        }

        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name cannot be empty.");

        var customer = new CustomerViewModel
        {
            CustomerID = id,
            Name = name
        };

        try
        {
            customerService.AddCustomer(customer);

            var cart = new ShoppingCartViewModel
            {
                CartID = GenerateId(),
                CustomerID = customer.CustomerID,
                Products = new List<ProductViewModel>()
            };
            shoppingCartService.AddShoppingCart(cart);

            Console.WriteLine($"Customer '{name}' signed up successfully!");
            Console.WriteLine($"A new shopping cart has been created for Customer ID: {customer.CustomerID}, Cart ID: {cart.CartID}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error during sign-up: {ex.Message}");
        }

        return customer;
    }


    public static EmployeeViewModel SignUpEmployee(IEmployeeService employeeService)
    {
        int id;
        while (true)
        {
            Console.Write("Enter new Employee ID: ");
            try
            {
                id = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Invalid ID format. Please enter a valid numeric ID.");
                continue;
            }

            if (employeeService.GetEmployeeById(id) != null)
            {
                Console.WriteLine("Employee ID already exists. Try a different ID.");
                continue;
            }
            break;
        }

        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name cannot be empty.");

        var employee = new EmployeeViewModel
        {
            EmployeeID = id,
            Name = name
        };

        try
        {
            employeeService.AddEmployee(employee);
            Console.WriteLine($"Employee '{name}' signed up successfully!");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error during sign-up: {ex.Message}");
        }

        return employee;
    }


    public static void CustomerMenu(int customerId, IProductService productService, IShoppingCartService shoppingCartService, IOrderService orderService)
    {
        while (true)
        {
            Console.WriteLine("\n=== Customer Menu ===");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Add Product to Cart");
            Console.WriteLine("3. View Cart");
            Console.WriteLine("4. Remove Product from Cart");
            Console.WriteLine("5. Place Order");
            Console.WriteLine("6. View My Orders");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option (1-7): ");

            string choice = Console.ReadLine();
            try
            {
                switch (choice)
                {
                    case "1": ViewProducts(productService); break;
                    case "2": AddProductToCart(customerId, productService, shoppingCartService); break;
                    case "3": ViewCart(customerId, shoppingCartService); break;
                    case "4": RemoveProductFromCart(customerId, productService, shoppingCartService); break;
                    case "5": PlaceOrder(customerId, shoppingCartService, orderService); break;
                    case "6": ViewCustomerOrders(customerId, orderService); break;
                    case "7": Console.WriteLine("Logging out..."); return;
                    default: Console.WriteLine("Invalid option. Choose 1-7."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    public static void EmployeeMenu(IProductService productService, ICustomerService customerService, IOrderService orderService)
    {
        while (true)
        {
            Console.WriteLine("\n=== Employee Menu ===");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. View All Customers");
            Console.WriteLine("4. View All Orders");
            Console.WriteLine("5. Update Order Status");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option (1-6): ");

            string choice = Console.ReadLine();
            try
            {
                switch (choice)
                {
                    case "1": AddProduct(productService); break;
                    case "2": ViewProducts(productService); break;
                    case "3": ViewAllCustomers(customerService); break;
                    case "4": ViewAllOrders(orderService); break;
                    case "5": UpdateOrderStatus(orderService); break;
                    case "6": Console.WriteLine("Logging out..."); return;
                    default: Console.WriteLine("Invalid option. Choose 1-6."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    public static void ViewProducts(IProductService productService)
    {
        var products = productService.GetAllProducts();
        if (products.Count == 0)
            throw new Exception("No products available.");
        Console.WriteLine("\nProducts:");
        foreach (var p in products)
            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}");
    }

    public static void AddProduct(IProductService productService)
    {
        Console.Write("Enter Product ID: ");
        int id;
        try
        {
            id = int.Parse(Console.ReadLine());
        }
        catch
        {
            throw new Exception("Invalid ID format.");
        }

        Console.Write("Enter Product Name: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Name cannot be empty.");

        Console.Write("Enter Product Price: ");
        decimal price;
        try
        {
            price = decimal.Parse(Console.ReadLine());
        }
        catch
        {
            throw new Exception("Invalid price format.");
        }
        if (price <= 0)
            throw new Exception("Price must be greater than zero.");

        var product = new ProductViewModel { Id = id, Name = name, Price = price };
        try
        {
            productService.AddProduct(product);
            Console.WriteLine($"Product '{name}' added successfully!");
        }
        catch
        {
            throw new Exception("Product ID already exists. Try a different ID.");
        }
    }

    public static void AddProductToCart(int customerId, IProductService productService, IShoppingCartService shoppingCartService)
    {
        ShoppingCartViewModel cart;

        cart = shoppingCartService.GetShoppingCartByCustomer(customerId);
        ViewProducts(productService);
        Console.Write("Enter Product ID to add to cart: ");
        int productId;
        try
        {
            productId = int.Parse(Console.ReadLine());
        }
        catch
        {
            throw new Exception("Invalid ID format.");
        }

        var product = productService.GetProductById(productId);
        cart.Products.Add(product);
        shoppingCartService.UpdateShoppingCart(cart);
        Console.WriteLine($"Product '{product.Name}' added to cart!");
    }

    public static void ViewCart(int customerId, IShoppingCartService shoppingCartService)
    {
        var cart = shoppingCartService.GetShoppingCartByCustomer(customerId);
        if (cart == null || cart.Products.Count == 0)
        {
            Console.WriteLine("Your cart is empty.");
            return;
        }

        Console.WriteLine("\nYour Cart:");
        decimal total = 0;
        foreach (var p in cart.Products)
        {
            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}");
            total += p.Price;
        }
        Console.WriteLine($"Total: {total}");
    }

    public static void RemoveProductFromCart(int customerId, IProductService productService, IShoppingCartService shoppingCartService)
    {
        var cart = shoppingCartService.GetShoppingCartByCustomer(customerId);
        if (cart == null || cart.Products.Count == 0)
        {
            Console.WriteLine("Your cart is empty.");
            return;
        }

        Console.WriteLine("\nYour Cart:");
        foreach (var p in cart.Products)
            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price}");

        Console.Write("Enter Product ID to remove from cart: ");
        int productId;
        try
        {
            productId = int.Parse(Console.ReadLine());
        }
        catch
        {
            throw new Exception("Invalid ID format.");
        }

        var product = cart.Products.Find(p => p.Id == productId);
        if (product == null)
        {
            Console.WriteLine("Product not found in cart.");
            return;
        }

        cart.Products.Remove(product);
        shoppingCartService.UpdateShoppingCart(cart);
        Console.WriteLine($"Product '{product.Name}' removed from cart!");
    }

    public static void PlaceOrder(int customerId, IShoppingCartService shoppingCartService, IOrderService orderService)
    {
        var cart = shoppingCartService.GetShoppingCartByCustomer(customerId);
        if (cart == null || cart.Products.Count == 0)
            throw new Exception("Cart is empty. Add products first.");

        Console.WriteLine("\nOrder Summary:");
        decimal total = 0;
        foreach (var p in cart.Products)
        {
            Console.WriteLine($"  - {p.Name}: {p.Price}");
            total += p.Price;
        }
        Console.WriteLine($"Total: {total}");

        Console.Write("Enter Payment Type ( 0-CreditCard  1-PayPal): ");
        int paymentType = int.Parse(Console.ReadLine());
        if (paymentType != 0 && paymentType != 1)
            throw new Exception("Invalid Payment.");

        var order = new OrderViewModel
        {
            OrderID = GenerateId(),
            CustomerID = customerId,
            Cart = cart,
            OrderDate = DateTime.Now,
            Status = OrderStatus.Pending,
            PaymentMethod = new PaymentViewModel
            {
                Type = (PaymentType)paymentType,
                TransactionID = GenerateId().ToString()
            }
        };

        orderService.AddOrder(order);
        Console.WriteLine($"Order placed successfully! Order ID: {order.OrderID}");

        cart.Products.Clear();
        shoppingCartService.UpdateShoppingCart(cart);
        Console.WriteLine("Cart cleared.");
    }

    public static void ViewCustomerOrders(int customerId, IOrderService orderService)
    {
        var orders = orderService.GetOrderByCustomer(customerId);
        if (orders.Count == 0)
        {
            Console.WriteLine("No orders found.");
            return;
        }

        Console.WriteLine("\nMy Orders:");
        foreach (var order in orders)
        {
            Console.WriteLine($"Order ID: {order.OrderID}, Date: {order.OrderDate:yyyy-MM-dd}, Status: {order.Status}");
            Console.WriteLine($"Payment: {order.PaymentMethod.Type}, Amount: {order.PaymentMethod.Amount}, Transaction: {order.PaymentMethod.TransactionID}");
            Console.WriteLine("Products:");
            foreach (var p in order.Cart.Products)
                Console.WriteLine($"  - {p.Name}: {p.Price}");
        }
    }

    public static void ViewAllCustomers(ICustomerService customerService)
    {
        var customers = customerService.GetAllCustomers();
        if (customers.Count == 0)
            throw new Exception("No customers found.");
        Console.WriteLine("\nCustomers:");
        foreach (var c in customers)
            Console.WriteLine($"ID: {c.CustomerID}, Name: {c.Name}");
    }

    public static void ViewAllOrders(IOrderService orderService)
    {
        var orders = orderService.GetAllOrders();
        if (orders.Count == 0)
            throw new Exception("No orders found.");
        Console.WriteLine("\nAll Orders:");
        foreach (var order in orders)
        {
            Console.WriteLine($"Order ID: {order.OrderID}, Customer ID: {order.CustomerID}, Date: {order.OrderDate:yyyy-MM-dd}, Status: {order.Status}");
        }
    }

    public static void UpdateOrderStatus(IOrderService orderService)
    {
        var orders = orderService.GetAllOrders();
        if (orders.Count == 0)
            throw new Exception("No orders found.");

        Console.WriteLine("\nAll Orders:");
        foreach (var order in orders)
        {
            Console.WriteLine($"Order ID: {order.OrderID}, Customer ID: {order.CustomerID}, Status: {order.Status}");
        }

        Console.Write("Enter Order ID to update: ");
        int orderId;
        try
        {
            orderId = int.Parse(Console.ReadLine());
        }
        catch
        {
            throw new Exception("Invalid ID format.");
        }

        var orderobj = orderService.GetOrderById(orderId);

        Console.WriteLine("Available Statuses: Pending, Shipped, Delivered, Cancelled");
        Console.WriteLine("Enter new status: (0-Pending 1-Processing 2-Shipped 3-Delivered)");
        int newStatus = int.Parse(Console.ReadLine());
        if (newStatus != 0 && newStatus != 1 && newStatus != 2 && newStatus != 3)
            throw new Exception("Invalid status. Use Pending, Processing, Shipped, Delivered");

        orderobj.Status = (OrderStatus)newStatus;
        orderService.UpdateOrder(orderobj);
        Console.WriteLine($"Order {orderId} status updated to {(OrderStatus)newStatus}!");
    }

    public static int GenerateId()
    {
        return new Random().Next(1000, 9999); // Simple random ID for demo
    }
}
