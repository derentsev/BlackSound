using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using BlackSoundDAL.Entities;

namespace BlackSoundDAL
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<User> GetAll()
        {
            List<User> resultSet = new List<User>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM usersTable";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        resultSet.Add(new User()
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"],
                            IsAdmin = (bool)reader["isAdmin"]
                        });
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return resultSet;
        }

        public User GetByID(int ID)
        {
            User userResult = new User();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM usersTable WHERE ID = @ID";

                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@ID";
                parameter.Value = ID;
                command.Parameters.Add(parameter);


                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {

                        userResult.ID = (int)reader["ID"];
                        userResult.Name = (string)reader["Name"];
                        userResult.Email = (string)reader["Email"];
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return userResult;
        }

        public void Insert(userTable user)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO usersTable (Name, Email, Password, isAdmin)VALUES (@Name, @Email, @Password, @isAdmin)";

            IDataParameter parameter = command.CreateParameter();
            parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = user.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Email";
            parameter.Value = user.Email;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Password";
            parameter.Value = user.Password;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@isAdmin";
            parameter.Value = user.isAdmin;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(userTable user)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE usersTable SET Name=@Name,Email=@Email, Password = @Password, isAdmin = @isAdmin WHERE ID=@ID";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@ID";
            parameter.Value = user.ID;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = user.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Email";
            parameter.Value = user.Email;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Password";
            parameter.Value = user.Password;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@isAdmin";
            parameter.Value = user.isAdmin;
            command.Parameters.Add(parameter);



            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Delete(int ID)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM usersTable WHERE ID=@ID";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@ID";
            parameter.Value = ID;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public bool LogIn(string userName, string password)
        {
            bool logged;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using (SqlCommand StrQuer = new SqlCommand("SELECT * FROM usersTable WHERE Name=@Name AND Password = @Password", connection))
            {
                StrQuer.Parameters.AddWithValue("@Name", userName);
                StrQuer.Parameters.AddWithValue("@Password", password);
                SqlDataReader dr = StrQuer.ExecuteReader();
                if (dr.HasRows)
                {
                    logged = true;
                }
                else
                {
                    logged = false;
                }
            }

            return logged;
        }
    }
}

