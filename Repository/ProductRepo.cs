using ADO.Net_App.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ADO.Net_App.Repository
{
    public class ProductRepo : IProductRepository
    {
        private static readonly string connectionString =
                 "Server=127.0.0.1;Port=3306;Database=ProductsDb;UserId=root;Password=1234";

        public void Initialize()
        {
            try
            {
                // Connect to MySQL server, NOT to a specific DB
                var builder = new MySqlConnectionStringBuilder(connectionString)
                {
                    Database = "" 
                };

                using (var connection = new MySqlConnection(builder.ToString()))
                {
                    connection.Open();
                    Console.WriteLine("Connection to MySQL server established");

                    using (var checkDb = new MySqlCommand("SHOW DATABASES LIKE 'ProductsDb';", connection))
                    using (var reader = checkDb.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Database already exists.");
                            reader.Close();
                        }
                        else
                        {
                            reader.Close();

                            //Create the database
                            using (var createDb = new MySqlCommand("CREATE DATABASE ProductsDb;", connection))
                            {
                                createDb.ExecuteNonQuery();
                                Console.WriteLine("Database created successfully.");
                            }
                        }
                    }

                    connection.Close();
                }

                // Connection to the newly created or existing database
                var dbBuilder = new MySqlConnectionStringBuilder(connectionString)
                {
                    Database = "ProductsDb"
                };

                using (var dbConnection = new MySqlConnection(dbBuilder.ToString()))
                {
                    dbConnection.Open();
                    Console.WriteLine("Connected to ProductsDb.");

                    // Create table if not exists
                    string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS products (
                Id INT PRIMARY KEY AUTO_INCREMENT,
                Name VARCHAR(255) NOT NULL,
                Quantity INT NOT NULL,
                Price DECIMAL(10,2) NOT NULL,
                Stock INT NOT NULL,
                Description TEXT
                 );
";

                    using (var createTable = new MySqlCommand(createTableQuery, dbConnection))
                    {
                        createTable.ExecuteNonQuery();
                        Console.WriteLine("Table 'products' checked/created successfully.");
                    }

                    dbConnection.Close();
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Connection Error: " + e.Message);
            }
        }

        public List<Product> GetAll()
        {
            Console.WriteLine("This method getAll is being called.");
            var products = new List<Product>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Fetching all products...");
                    string query = "SELECT * FROM products";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDouble(reader["Price"]),
                                Stock = Convert.ToInt32(reader["Stock"]),
                                Description = reader["Description"].ToString()
                            });
                        }
                    }
                }

                Console.WriteLine($"Found {products.Count} products.");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("GetAll products error: " + e.Message);
            }

            return products;
        }

        public Product GetById(int id)
        {
            Product product = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products WHERE Id = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                product = new Product
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Price = Convert.ToDouble(reader["Price"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    Description = reader["Description"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("GetById error: " + e.Message);
            }
            return product;
        }

        public void Add(Product product)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO products (Name, Quantity, Price, Stock, Description) VALUES (@Name, @Quantity, @Price, @Stock, @Description)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Stock", product.Stock);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Add product error: " + e.Message);
            }
        }

        public void Update(Product product)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Products SET Name = @Name, Quantity = @Quantity, Price = @Price, Stock = @Stock, Description = @Description WHERE Id = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", product.Id);
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Stock", product.Stock);
                        cmd.Parameters.AddWithValue("@Description", product.Description);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Update product error: " + e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE Id = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Delete product error: " + e.Message);
            }
        }

        public void PatchProduct(Product product)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Products SET ";

                    var updates = new List<string>();
                    var parameters = new List<MySqlParameter>();

                    if (!string.IsNullOrEmpty(product.Name))
                    {
                        updates.Add("Name = @Name");
                        parameters.Add(new MySqlParameter("@Name", product.Name));
                    }
                    if (product.Quantity != 0)
                    {
                        updates.Add("Quantity = @Quantity");
                        parameters.Add(new MySqlParameter("@Quantity", product.Quantity));
                    }
                    if (product.Price != 0)
                    {
                        updates.Add("Price = @Price");
                        parameters.Add(new MySqlParameter("@Price", product.Price));
                    }
                    if (product.Stock != 0)
                    {
                        updates.Add("Stock = @Stock");
                        parameters.Add(new MySqlParameter("@Stock", product.Stock));
                    }
                    if (!string.IsNullOrEmpty(product.Description))
                    {
                        updates.Add("Description = @Description");
                        parameters.Add(new MySqlParameter("@Description", product.Description));
                    }

                    if (updates.Count > 0)
                    {
                        query += string.Join(", ", updates) + " WHERE Id = @Id";
                        parameters.Add(new MySqlParameter("@Id", product.Id));

                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("PatchProduct error: " + e.Message);
            }
        }
    }
}
