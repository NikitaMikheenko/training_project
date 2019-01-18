using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class NoteContext : INoteContext
    {
        public int? AddNote(Note model)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                string str = $"EXEC AddNote '{model.Name}', '{model.Date.Year}-{model.Date.Month}-{model.Date.Day}'," +
                    $"'{model.Description}', {model.Category.Id}, '{model.Imglink}', {model.User.Id}";

                SqlCommand command = new SqlCommand(str, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    return (int?)reader["Id"];
                }

                return null;
            }
        }

        public bool AddNoteLink(int noteId, int linkedNoteId)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                string str = $"EXEC AddNoteLink {noteId}, {linkedNoteId}";

                SqlCommand command = new SqlCommand(str, connection);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public bool DeleteNoteById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC DeleteNoteById {id}", connection);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public bool DeleteNoteLink(int noteId, int linkedNoteId)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC DeleteNoteLink {noteId}, {linkedNoteId}", connection);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public void DeleteNoteLinkByLinkedNoteId(int linkedNoteId)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC DeleteNoteLinkByLinkedNoteId {linkedNoteId}", connection);

                int n = command.ExecuteNonQuery();
            }
        }

        public bool EditNote(Note model)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                string str = $"EXEC EditNote {model.Id}, '{model.Name}', '{model.Date.Year}-{model.Date.Month}-{model.Date.Day}'," +
                    $"'{model.Description}', {model.Category.Id}, '{model.Imglink}'";

                SqlCommand command = new SqlCommand(str, connection);

                return command.ExecuteNonQuery() == 1;
            }
        }

        public List<NoteInfoModel> GetLinkedNotesInfoByNoteId(int id)
        {
            List<NoteInfoModel> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetLinkedNotesInfoByNoteId {id}", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<NoteInfoModel>();

                    while (reader.Read())
                    {
                        model.Add(new NoteInfoModel()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Date = Convert.ToDateTime(reader["NoteDate"]),
                            Category = new Category()
                            {
                                Id = (int)reader["CategoryId"],
                                Name = (string)reader["CategoryName"]
                            }
                        });
                    }

                    return model;
                }
            }

            return null;
        }

        public Note GetNoteById(int id)
        {
            Note model = new Note();

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetNoteById {id}", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    model = new Note()
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Date = Convert.ToDateTime(reader["NoteDate"]),
                        Category = new Category()
                        {
                            Id = (int)reader["CategoryId"],
                            Name = (string)reader["CategoryName"]
                        },
                        User = new UserInfoModel()
                        {
                            Id = (int)reader["UserId"],
                            Login = (string)reader["UserLogin"],
                            Role = new Role()
                            {
                                Id = (int)reader["RoleId"],
                                Name = (string)reader["RoleName"]
                            }
                        }
                    };

                    var description = reader["Description"];
                    var imgLink = reader["ImgLink"];

                    if (description != DBNull.Value)
                    {
                        model.Description = (string)description;
                    }

                    if (imgLink != DBNull.Value)
                    {
                        model.Imglink = (string)imgLink;
                    }
                }
                else
                {
                    return null;
                }
            }

            model.LinkedNotes = GetLinkedNotesInfoByNoteId((int)model.Id);

            return model;
        }

        public List<NoteInfoModel> GetNotesInfoByCategoryId(int id)
        {
            List<NoteInfoModel> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetNotesInfoByCategoryId {id}", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<NoteInfoModel>();

                    while (reader.Read())
                    {
                        model.Add(new NoteInfoModel()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Date = Convert.ToDateTime(reader["NoteDate"]),
                            Category = new Category()
                            {
                                Id = (int)reader["CategoryId"],
                                Name = (string)reader["CategoryName"]
                            },
                            User = new UserInfoModel()
                            {
                                Id = (int)reader["UserId"],
                                Login = (string)reader["UserLogin"],
                                Role = new Role()
                                {
                                    Id = (int)reader["RoleId"],
                                    Name = (string)reader["RoleName"]
                                }
                            }
                        });
                    }

                    return model;
                }
            }

            return null;
        }

        public List<NoteInfoModel> GetNotesInfoByDate(DateTime date)
        {
            List<NoteInfoModel> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetNotesInfoByDate '{date.Date.Year}-{date.Date.Month}-{date.Date.Day}'", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<NoteInfoModel>();

                    while (reader.Read())
                    {
                        model.Add(new NoteInfoModel()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Date = Convert.ToDateTime(reader["NoteDate"]),
                            Category = new Category()
                            {
                                Id = (int)reader["CategoryId"],
                                Name = (string)reader["CategoryName"]
                            },
                            User = new UserInfoModel()
                            {
                                Id = (int)reader["UserId"],
                                Login = (string)reader["UserLogin"],
                                Role = new Role()
                                {
                                    Id = (int)reader["RoleId"],
                                    Name = (string)reader["RoleName"]
                                }
                            }
                        });
                    }

                    return model;
                }
            }

            return null;
        }

        public List<NoteInfoModel> GetNotesInfoByName(string name)
        {
            List<NoteInfoModel> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetNotesInfoByName '{name}'", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<NoteInfoModel>();

                    while (reader.Read())
                    {
                        model.Add(new NoteInfoModel()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Date = Convert.ToDateTime(reader["NoteDate"]),
                            Category = new Category()
                            {
                                Id = (int)reader["CategoryId"],
                                Name = (string)reader["CategoryName"]
                            },
                            User = new UserInfoModel()
                            {
                                Id = (int)reader["UserId"],
                                Login = (string)reader["UserLogin"],
                                Role = new Role()
                                {
                                    Id = (int)reader["RoleId"],
                                    Name = (string)reader["RoleName"]
                                }
                            }
                        });
                    }

                    return model;
                }
            }

            return null;
        }

        public List<NoteInfoModel> GetNotesInfoByUserId(int id)
        {
            List<NoteInfoModel> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetNotesInfoByUserId {id}", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<NoteInfoModel>();

                    while (reader.Read())
                    {
                        model.Add(new NoteInfoModel()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Date = Convert.ToDateTime(reader["NoteDate"]),
                            Category = new Category()
                            {
                                Id = (int)reader["CategoryId"],
                                Name = (string)reader["CategoryName"]
                            },
                            User = new UserInfoModel()
                            {
                                Id = (int)reader["UserId"],
                                Login = (string)reader["UserLogin"],
                                Role = new Role()
                                {
                                    Id = (int)reader["RoleId"],
                                    Name = (string)reader["RoleName"]
                                }
                            }
                        });
                    }

                    return model;
                }
            }

            return null;
        }

        public List<NoteInfoModel> GetUserNotesInfoByCategoryId(int userId, int categoryId)
        {
            List<NoteInfoModel> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetUserNotesInfoByCategoryId {userId}, {categoryId}", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<NoteInfoModel>();

                    while (reader.Read())
                    {
                        model.Add(new NoteInfoModel()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Date = Convert.ToDateTime(reader["NoteDate"]),
                            Category = new Category()
                            {
                                Id = (int)reader["CategoryId"],
                                Name = (string)reader["CategoryName"]
                            },
                            User = new UserInfoModel()
                            {
                                Id = (int)reader["UserId"],
                                Login = (string)reader["UserLogin"],
                                Role = new Role()
                                {
                                    Id = (int)reader["RoleId"],
                                    Name = (string)reader["RoleName"]
                                }
                            }
                        });
                    }

                    return model;
                }
            }

            return null;
        }

        public List<NoteInfoModel> GetUserNotesInfoByDate(int userId, DateTime date)
        {
            List<NoteInfoModel> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetUserNotesInfoByDate {userId}, '{date.Date.Year}-{date.Date.Month}-{date.Date.Day}'", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<NoteInfoModel>();

                    while (reader.Read())
                    {
                        model.Add(new NoteInfoModel()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Date = Convert.ToDateTime(reader["NoteDate"]),
                            Category = new Category()
                            {
                                Id = (int)reader["CategoryId"],
                                Name = (string)reader["CategoryName"]
                            },
                            User = new UserInfoModel()
                            {
                                Id = (int)reader["UserId"],
                                Login = (string)reader["UserLogin"],
                                Role = new Role()
                                {
                                    Id = (int)reader["RoleId"],
                                    Name = (string)reader["RoleName"]
                                }
                            }
                        });
                    }

                    return model;
                }
            }

            return null;
        }

        public List<NoteInfoModel> GetUserNotesInfoByName(int userId, string name)
        {
            List<NoteInfoModel> model;

            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"EXEC GetUserNotesInfoByName {userId}, '{name}'", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    model = new List<NoteInfoModel>();

                    while (reader.Read())
                    {
                        model.Add(new NoteInfoModel()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Date = Convert.ToDateTime(reader["NoteDate"]),
                            Category = new Category()
                            {
                                Id = (int)reader["CategoryId"],
                                Name = (string)reader["CategoryName"]
                            },
                            User = new UserInfoModel()
                            {
                                Id = (int)reader["UserId"],
                                Login = (string)reader["UserLogin"],
                                Role = new Role()
                                {
                                    Id = (int)reader["RoleId"],
                                    Name = (string)reader["RoleName"]
                                }
                            }
                        });
                    }

                    return model;
                }
            }

            return null;
        }

        public string GetImgLinkByNoteId(int id)
        {
            using (SqlConnection connection = new SqlConnection(Context.ConnectionString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT Notes.ImgLink FROM Notes WHERE Id = {id}", connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    return (string)reader["ImgLink"];
                }
            }

            return null;
        }
    }
}
