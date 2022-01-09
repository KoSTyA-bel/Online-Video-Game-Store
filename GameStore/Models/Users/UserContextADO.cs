using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GameStore.Models.Users
{
    public class UserContextADO : IUserContext
    {
        private readonly string _connectionString;

        public UserContextADO(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public int AddUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandText = "InsertUser",
                Connection = connection,
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@Login", SqlDbType.NVarChar, 40));
            command.Parameters["@Login"].Value = user.Login;
            command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 40));
            command.Parameters["@Password"].Value = user.Password;
            command.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int, 4));
            command.Parameters["@RoleId"].Value = user.RoleId;
            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            command.Parameters["@Id"].Direction = ParameterDirection.Output;

            using (connection)
            {
                connection.Open();
                command.ExecuteNonQuery();
                return (int)command.Parameters["@Id"].Value;
            }
        }

        public void UpdateUser(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandText = "UpdateUser",
                Connection = connection,
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            command.Parameters["@Id"].Value = user.Id;
            command.Parameters.Add(new SqlParameter("@Login", SqlDbType.NVarChar, 40));
            command.Parameters["@Login"].Value = user.Login;
            command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 40));
            command.Parameters["@Password"].Value = user.Password;
            command.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int, 4));
            command.Parameters["@RoleId"].Value = user.RoleId;

            using (connection)
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandText = "DeleteUser",
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
            };

            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            command.Parameters["@Id"].Value = id;

            using (connection)
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public User SelectUser(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandText = "GetUser",
                CommandType = CommandType.StoredProcedure,
                Connection = connection,
            };

            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            command.Parameters["@Id"].Value = id;

            using (connection)
            {
                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();

                var user = new User()
                {
                    Id = (int)reader["Id"],
                    Login = (string)reader["Login"],
                    Password = (string)reader["Password"],
                    RoleId = (int?)reader["RoleId"],
                };

                reader.Close();

                return user;
            }
        }

        public User SelectUser(string login)
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandText = "GetUserByLogin",
                CommandType = CommandType.StoredProcedure,
                Connection = connection,
            };

            command.Parameters.Add(new SqlParameter("@Login", SqlDbType.NVarChar, 40));
            command.Parameters["@Login"].Value = login;

            using (connection)
            {
                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();

                var user = new User()
                {
                    Id = (int)reader["Id"],
                    Login = (string)reader["Login"],
                    Password = (string)reader["Password"],
                    RoleId = (int?)reader["RoleId"],
                };

                reader.Close();

                return user;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandText = "GetAllUsers",
                CommandType = CommandType.StoredProcedure,
                Connection = connection,
            };
            var users = new List<User>();

            using (connection)
            {
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User()
                    {
                        Id = (int)reader["Id"],
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"],
                        RoleId = (int?)reader["RoleId"],
                    });
                }

                reader.Close();

                return users;
            }
        }

        public int GetCountOfUsers()
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandText = "GetCountOfUsers",
                CommandType = CommandType.StoredProcedure,
                Connection = connection,
            };

            using (connection)
            {
                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.SingleRow);

                reader.Read();

                var count = reader.GetInt32(0);

                reader.Close();

                return count;
            }
        }

        public Role SelectRole(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand()
            {
                CommandText = "GetRole",
                CommandType = CommandType.StoredProcedure,
                Connection = connection,
            };

            command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
            command.Parameters["@Id"].Value = id;

            using (connection)
            {
                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();

                var role = new Role()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                };

                return role;
            }
        }
    }
}
