using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackSoundDAL.Entities;

namespace BlackSoundDAL.Repositories
{
    class ArtistRepository
    {
        private readonly string connectionString;

        public ArtistRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Artist> GetAll()
        {
            List<Artist> resultset = new List<Artist>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM artistTable";
                IDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        resultset.Add(new Artist
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            Country = (string)reader["Country"]
                        });
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return resultset;

        }

        public List<Artist> GetByID()
        {
            List<Artist> resultset = new List<Artist>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT FROM artistTable WHERE ID = @ID";
                IDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while(reader.Read())
                    {
                        resultset.Add(new Artist
                        {
                            ID = (int)reader["ID"],
                            Country = (string)reader["Country"],
                            Name = (string)reader["Name"]

                        });
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return resultset;
        }

        public void Insert(Artist artist)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            IDbCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO artistTable(Name, Country) VALUES (@Name, @Country)";

            IDataParameter parameter = command.CreateParameter();
            parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = artist.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Country";
            parameter.Value = artist.Country;
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

        public void Update(Artist artist)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            IDbCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE artistTable  SET Name = @Name, Country = @Country WHERE ID=@ID";

            IDataParameter parameter = command.CreateParameter();
            parameter = command.CreateParameter();
            parameter.ParameterName = "Name";
            parameter.Value = artist.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "Country";
            parameter.Value = artist.Country;
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

        public void Delete(Artist artist)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM artistTable WHERE ID=@ID";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@ID";
            parameter.Value = artist.ID;
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
