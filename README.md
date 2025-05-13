# E-Commerce Application

This is a console-based E-Commerce application built using C# and .NET 8. It allows users to interact as either customers or employees, providing features such as product management, shopping cart operations, and order processing.

## Features
- **Customer Features**:
  - Sign up and log in as customer.
  - View products
  - Add products to the shopping cart
  - Place orders
  - View order history
- **Employee Features**:
  - Sign up and log in as employee. 
  - Add and manage products
  - View all customers and orders
  - Update order statuses

## Prerequisites
- .NET 8 SDK installed on your machine
- Visual Studio 2022 or later

## How to Run the Project
1. Clone the repository:
2. Open the solution in Visual Studio 2022.
3. Build the solution to restore dependencies.
4. Ensure the required JSON files (`employees.json`, `customers.json`, etc.) are present in the appropriate directory.
5. Run the project.

## Assumptions
- The application uses JSON files as a simple data storage mechanism. 
- Employee and customer IDs are unique integers.
- The `GenerateId` method is used to create random IDs for new entities.
- The application assumes valid input for most operations but includes basic error handling for invalid formats.

## Design Decisions
- **Layered Architecture**: The project is divided into layers (Presentation, Business, Data) to separate concerns and improve maintainability.
- **View Models**: View models are used to decouple the business logic from the data layer, providing a simplified and UI-friendly representation of the data.
- **Repository Pattern**: Repositories (e.g., IShoppingCartRepository, IProductRepository) abstract data access, making it easier to switch to a different data source, such as a database, in the future.
- **Sign-Up and Log-In Functionality**: Repositories (e.g., IShoppingCartRepository, IProductRepository) abstract data access, making it easier to switch to a different data source, such as a database, in the                                            future.
- **Service Factory**: The ServiceFactory class centralizes the creation of service objects (e.g., IEmployeeService, IShoppingCartService), simplifying dependency management and ensuring consistent service                              instantiation.
- **Error Handling**: The application includes basic error handling to manage invalid inputs and unexpected scenarios, such as null values or invalid formats.
- **Console-Based UI**: A simple console interface is used for demonstration purposes.
