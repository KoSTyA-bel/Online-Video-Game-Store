﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace GameStore.Models.Products
{
    public class ProductContextADO : IProductContext
    {
        private readonly string _connectionString;
        private readonly string _getAllProducts = "SELECT * FROM Products";
        private readonly string _selectProduct = "SELECT * FROM Products WHERE Id = @Id";
        private readonly string _selectProductByName = "SELECT * FROM Products WHERE Name LIKE '%@NAME%'";
        private readonly string _updateProduct = "UPDATE Products SET Name = @Name, Price = @Price, Description = @Description, PathToPicture = @PathToPicture WHERE Id = @Id";
        private readonly string _deleteProduct = "DELETE FROM Products WHERE Id = @Id";
        private readonly string _countOfProducts = "SELECT COUNT(ID) FROM Products";

        public ProductContextADO(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public int AddProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                Connection = connection,
                CommandText = "AddProduct",
            };

            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
            command.Parameters["@Name"].Value = product.Name;
            command.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
            command.Parameters["@Description"].Value = product.Description;
            command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal));
            command.Parameters["@Price"].Value = product.Price;
            command.Parameters.Add(new SqlParameter("@PathToPicture", SqlDbType.NVarChar));
            command.Parameters["@PathToPicture"].Value = product.PathToPicture;
            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            command.Parameters["@Id"].Direction = ParameterDirection.Output;

            using (connection)
            {
                connection.Open();
                command.ExecuteNonQuery();
                return (int)command.Parameters["@Id"].Value;
            }
        }

        public void UpdateProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                Connection = connection,
                CommandText = _updateProduct,
            };

            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            command.Parameters["@Id"].Value = product.Id;
            command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
            command.Parameters["@Name"].Value = product.Name;
            command.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
            command.Parameters["@Description"].Value = product.Description;
            command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal));
            command.Parameters["@Price"].Value = product.Price;
            command.Parameters.Add(new SqlParameter("@PathToPicture", SqlDbType.NVarChar));
            command.Parameters["@PathToPicture"].Value = product.PathToPicture;

            using (connection)
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                Connection = connection,
                CommandText = _deleteProduct,
            };

            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            command.Parameters["@Id"].Value = id;

            using (connection)
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Product SelectProduct(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                Connection = connection,
                CommandText = _selectProduct,
            };

            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
            command.Parameters["@Id"].Value = id;

            using (connection)
            {
                connection.Open();
                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();

                var product = new Product()
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Description = (string)reader["Description"],
                    Price = (decimal)reader["Price"],
                    PathToPicture = (string)reader["PathToPicture"],
                };

                reader.Close();

                return product;
            }
        }

        public int GetCountOfProducts()
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandText = _countOfProducts,
                Connection = connection,
            };

            using (connection)
            {
                connection.Open();

                using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    return reader.GetInt32(0);
                }
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandText = _getAllProducts,
                Connection = connection,
            };
            var products = new List<Product>();

            using (connection)
            {
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product()
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        Price = (decimal)reader["Price"],
                        PathToPicture = (string)reader["PathToPicture"],
                    });
                }

                reader.Close();

                return products;
            }
        }
    }
}
