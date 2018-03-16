﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BlackSoundDAL
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<userTable> GetAll()
        {
            List<userTable> resultSet = new List<userTable>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM userTable";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        //Fill isAdmin and pass
                        //Finish GetByID
                        resultSet.Add(new userTable()
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"]
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

        public List<userTable> GetByID(userTable user)
        {
            List<userTable> resultSet = new List<userTable>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT  FROM userTable WHERE ID = @ID";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        resultSet.Add(new userTable()
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            Email = (string)reader["Email"]
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

        public void Insert(userTable user)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Contacts (Name, Email, Password, isAdmin)VALUES (@Name, @Email, @Password, @isAdmin)";

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
            command.CommandText = "UPDATE Contacts SET Name=@Name,Email=@Email, Password = @Password, isAdmin = @isAdmin WHERE ID=@ID";

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

        public void Delete(userTable  user)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText ="DELETE FROM Contacts WHERE ID=@ID";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@ID";
            parameter.Value = user.ID;
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
    }
}

    }
}
