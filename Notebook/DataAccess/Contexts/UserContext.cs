using System.Collections.Generic;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class UserContext : IUserContext
    {
        public bool AddUser(User model)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC AddUser '{model.Login}', '{model.Password}', {model.Role.Id}", connection);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public bool DeleteUserById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC DeleteUserById {id}", connection);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public bool EditUser(User model)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC EditUser {model.Id}, '{model.Login}', '{model.Password}', {model.Role.Id}", connection);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public User GetUserByLogin(string login)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetUserByLogin '{login}'", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    return new User()
                    {
                        Id = (int)reader["Id"],
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"],
                        Role = new Role()
                        {
                            Id = (int)reader["RoleId"],
                            Name = (string)reader["RoleName"]
                        }
                    };
                }
            }

            return null;
        }

        public List<UserInfoModel> GetUsersInfo()
        {
            List<UserInfoModel> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetUsersInfo", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<UserInfoModel>();

                    while (reader.Read())
                    {
                        model.Add(new UserInfoModel()
                        {
                            Id = (int)reader["Id"],
                            Login = (string)reader["Login"],
                            Role = new Role() { Id = (int)reader["RoleId"], Name = (string)reader["RoleName"] }
                        });
                    }
                    
                    return model;
                }
            }

            return null;
        }

        public int? GetUserIdByLogin(string login)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT Users.Id FROM Users WHERE Login = '{login}'", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    return (int)reader["Id"];
                }
            }

            return null;
        }

        public int? GetUserRoleIdByLogin(string login)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT Users.RoleId FROM Users WHERE Login = '{login}'", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    return (int)reader["RoleId"];
                }
            }

            return null;
        }
    }
}
