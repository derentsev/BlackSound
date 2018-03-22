using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackSoundDAL.Entities;

namespace BlackSoundDAL.Repositories
{
    public class SongRepository
    {
        private readonly string connectionString;

        public SongRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Song> GetAll()
        {
            List<Song> resultSet = new List<Song>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM songsTable";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        resultSet.Add(new Song
                        {
                            ID = (int)reader["ID"],
                            Name = (string)reader["Name"],
                            Year = (int)reader["YearCreated"],
                            ArtistName = (string)reader["ArtistName"]
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

        public Song GetByID(int ID)
        {
            Song songResult = new Song();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM songsTable WHERE ID = @ID";

                IDataParameter parameter = command.CreateParameter();
                parameter = command.CreateParameter();
                parameter.ParameterName = "@ID";
                parameter.Value = ID;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {

                        songResult.ID = (int)reader["ID"];
                        songResult.Name = (string)reader["Name"];
                        songResult.ArtistName = (string)reader["ArtistName"];
                        songResult.Year = (int)reader["YearCreated"];
                    }
                }
            }

            finally
            {
                connection.Close();
            }

            return songResult;
        }

        public void Insert(Song song)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO songsTable (Name, ArtistName, YearCreated) VALUES (@Name, @ArtistName, @YearCreated)";

            IDataParameter parameter = command.CreateParameter();
            parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = song.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ArtistName";
            parameter.Value = song.ArtistName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@YearCreated";
            parameter.Value = song.Year;
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

        public void Update(Song song)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE songsTable SET Name = @Name, ArtistName = @ArtistName, YearCreated = @YearCreated WHERE ID = @ID";

            IDataParameter parameter = command.CreateParameter();
            parameter = command.CreateParameter();
            parameter.ParameterName = "@Name";
            parameter.Value = song.Name;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ID";
            parameter.Value = song.ID;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@YearCreated";
            parameter.Value = song.Year;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ArtistName";
            parameter.Value = song.ArtistName;
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
            command.CommandText = "DELETE FROM songsTable WHERE ID=@ID";

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
    }
}
