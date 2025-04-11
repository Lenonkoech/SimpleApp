
### SimpleAPP - Product Management System

This is a simple Product Management System built using ASP.NET Core, ADO.NET, and MySQL. The application supports CRUD operations for managing products, such as adding, editing, deleting, and listing products in the database. It also includes automatic database creation and table generation from models.

### Features

- **CRUD Operations**: 
  - Add, Edit, Delete, and View products.
  - Product details include `Id`, `Name`, `Quantity`, `Price`, `Stock`, and `Description`.
  
- **Automatic Database Creation**:
  - When the application starts, it automatically checks if the database exists. If it doesn't, it creates the database and the necessary tables based on the model.

- **Dynamic Table Creation**:
  - The application dynamically creates the `products` table in MySQL based on the existing model (`Product`).

- **Auto Increment**:
  - The `Id` field in the `products` table is set to auto-increment to avoid manual input of IDs.

- **Error Handling**:
  - The application catches MySQL exceptions and logs errors to the console if the database connection or table creation fails.

### Prerequisites

- .NET 6 or higher
- MySQL Server
- Visual Studio or any preferred IDE

#### Setup Instructions

1. **Clone the Repository**:
   Clone the repository to your local machine.

   ```bash
   git clone https://github.com/LenonKoech/SimpleApp.git
   cd SimpleApp
   ```

2. **Configure Database Connection**:
   - Open the `appsettings.json` file in your project and configure the MySQL connection string as shown below:
   
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=ProductsDb;User=root;Password=yourpassword;"
   }
   ```

3. **Run the Application**:
   Run the application using Visual Studio or the .NET CLI. Upon startup, the application will attempt to create the `ProductsDb` database and the `products` table if they don't already exist.

   ```bash
   dotnet run

## How It Works

### 1. Database Initialization

Upon the first run, the application will check if the `ProductsDb` database exists. If not, it will create the database and then create the `products` table based on the `Product` model.

### 2. Adding Products

You can add products through the interface. The `Id` field will auto-increment, so you don't need to provide it when adding a new product.

### 3. Editing and Deleting Products

Products can be edited or deleted from the product list page. When deleting a product, you will be asked to confirm the deletion.

### 4. Error Handling

If there are any issues with connecting to the database or executing SQL commands, an error message will be displayed in the console.

## Technologies Used

- **ASP.NET Core**: Web framework for building the application.
- **ADO.NET**: Data access technology used to interact with MySQL.
- **MySQL**: Database management system for storing product information.
- **Bootstrap**: Frontend CSS framework for responsive and styled UI components.

```
This `README.md` should provide clear instructions on how to set up the project,
use the database, and take advantage of the features you've implemented,
such as automatic database creation and table generation.
```
   ```
✌✌😎 Happy Coding! ✌✌
```