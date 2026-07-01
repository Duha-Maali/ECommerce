# E-Commerce Application
A console-based e-commerce application developed with C# and .NET 8 as part of a backend development internship assignment.

The project applies a three-layer architecture by separating Presentation, Business, and Data layers. It supports customer and employee workflows, product management, shopping cart operations, order processing, and JSON-based data persistence.

## Features

**Customer Features**:
- Create a customer account
- Log in as a customer
- Browse available products
- Add products to the shopping cart
- View and update the shopping cart
- Submit orders

**Employee Features**:
- Create an employee account
- Log in as an employee
- Add products
- Update product information
- Delete products
- View customer orders

## Design Decisions

- **Layered Architecture**: The project is divided into layers (Presentation, Business, Data) to separate concerns and improve maintainability.
- **View Models**: View models provide simplified representations of data for the business and presentation layers without exposing data entities directly.
- **Repository Pattern**: Repository interfaces and implementations abstract data-access operations and separate them from the business layer.
- **Sign-Up and Log-In Functionality**: Customers and employees can create accounts and log in through separate workflows. User data is persisted in JSON files and validated before access is granted.
- **Service Layer**: Service classes contain the application’s business logic and coordinate operations between the presentation and data layers.

## Data Storage

The application uses JSON files as a lightweight persistence mechanism:

- `customers.json` — stores customer accounts
- `employees.json` — stores employee accounts
- `products.json` — stores product data
- `shoppingcarts.json` — stores shopping cart data
- `orders.json` — stores submitted orders

JSON storage was used for training purposes instead of a relational database.

## Assumptions
- Employee and customer IDs are unique integers.
- The `GenerateId` method is used to create random IDs for new entities.
- The application expects valid input for most operations but includes basic error handling for invalid formats and null values.
  
## How to Run the Project

### Prerequisites
- .NET 8 SDK installed on your machine
- Visual Studio 2022 or later

### Steps
1. Clone the repository: git clone https://github.com/Duha-Maali/ECommerce.git
2. Open ECommerce.sln in Visual Studio.
3. Set Ecommerce.Presentation as the startup project
4. Build the solution to restore dependencies.
5. Ensure the required JSON files (`employees.json`, `customers.json`, etc.) are present in the appropriate directory.
6. Run the project.


