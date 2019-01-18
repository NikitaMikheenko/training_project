using System.Collections.Generic;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class RoleContext : IRoleContext
    {
        public List<Role> GetRoles()
        {
            List<Role> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetRoles", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<Role>();

                    while (reader.Read())
                    {
                        model.Add(new Role()
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
    }
}
