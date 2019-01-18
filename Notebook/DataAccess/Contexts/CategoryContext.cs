using System.Collections.Generic;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class CategoryContext : ICategoryContext
    {
        public bool AddCategory(Category model)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC AddCategory '{model.Name}'", connection);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public bool DeleteCategoryById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC DeleteCategoryById {id}", connection);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public bool EditCategory(Category model)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC EditCategory {model.Id}, '{model.Name}'", connection);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetCategories", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<Category>();

                    while (reader.Read())
                    {
                        model.Add(new Category()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"]
                        });
                    }

                    return model;
                }
            }

            return null;
        }

        public Category GetCategoryById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetCategoryById {id}", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    return new Category()
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"]
                    };
                }
            }

            return null;
        }

        public int? GetCategoryIdByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT Categories.Id FROM Categories WHERE Name = '{name}'", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    return (int)reader["Id"];
                }
            }

            return null;
        }
    }
}
